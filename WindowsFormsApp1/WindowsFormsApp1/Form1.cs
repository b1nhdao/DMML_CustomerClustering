using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.ML;
using Microsoft.ML.Data;
using OxyPlot;
using OxyPlot.Series;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        public class DataRFM
        {
            [LoadColumn(0)]
            public string CustomerID { get; set; }

            [LoadColumn(1)]
            public float Recency { get; set; }

            [LoadColumn(2)]
            public float Frequency { get; set; }

            [LoadColumn(3)]
            public float Monetary { get; set; }
        }

        public class ClusterPrediction
        {
            [ColumnName("PredictedLabel")]
            public uint ClusterID { get; set; }
        }

        private void btn_button1_Click(object sender, EventArgs e)
        {
            string day = mtb_day.Text.ToString();
            int frequency = Convert.ToInt32(mtb_freq.Text.ToString());
            float monetary = float.Parse(mtb_money.Text.Replace("$", "").Trim());

            int daysDifference = 0;

            if (DateTime.TryParseExact(day, "MM/dd/yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime selectedDate))
            {
                DateTime currentDate = DateTime.Now;

                daysDifference = (currentDate - selectedDate).Days;
                if (daysDifference > 365)
                    daysDifference = 365;
                if (daysDifference < 0)
                {
                    MessageBox.Show("Ngày không hợp lệ !");
                    return;
                }

                //MessageBox.Show("Khoảng cách là: " + daysDifference + " ngày");
            }

            float recencyScaled = minMaxScalerRecency((float) daysDifference);
            float frequencyScaled = minMaxScalerFrequency((float) frequency);
            float monetaryScaled = minMaxScalerMonetary((float) monetary);

            //tb_ketqua.Text = recency.ToString();
            MessageBox.Show("recency: " + recencyScaled + "\n" +
                "frequency: " + frequencyScaled + "\n" +
                "monetary: " + monetaryScaled);

            var mlContext = new MLContext();
            string modelPath = "C:\\Users\\Admin\\source\\repos\\BTL_KMeans\\BTL_KMeans\\Output\\ModelKMeans.zip";

            // Tải mô hình đã huấn luyện
            ITransformer trainedModel = mlContext.Model.Load(modelPath, out var modelSchema);

            var sampleCustomer = new DataRFM
            {
                Recency = recencyScaled,
                Frequency = frequencyScaled,
                Monetary = monetaryScaled
            };

            var predictionEngine = mlContext.Model.CreatePredictionEngine<DataRFM, ClusterPrediction>(trainedModel);
            var prediction = predictionEngine.Predict(sampleCustomer);

            // Hiển thị cụm dự đoán
            tb_ketqua.Text = $"Khách hàng thuộc cụm: {prediction.ClusterID}";
        }

        private float minMaxScalerRecency(float data)
        {
            data = (float)((data) / 365) ;
            return data;
        }

        private float minMaxScalerFrequency(float data)
        {
            if (data > 958)
                data = 958;
            data = (float)(data / 959);
            return data;
        }

        
        private float minMaxScalerMonetary(float data)
        {
            if (data > 5861)
                data = 5861;
            data = ((float)((float)(data) / 5861));
            return data;
        }

        private void btn_chooseFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Select File";
            openFileDialog.InitialDirectory = @"C:\";
            openFileDialog.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
            openFileDialog.FilterIndex = 0;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                tb_file.Text = openFileDialog.FileName;

            }
        }

        private void PredictFromCsvFile(string filePath)
        {
            var mlContext = new MLContext();
            string modelPath = "C:\\Users\\Admin\\source\\repos\\BTL_KMeans\\BTL_KMeans\\Output\\ModelKMeans.zip";

            // Load trained model
            ITransformer trainedModel = mlContext.Model.Load(modelPath, out var modelSchema);

            // Read data from CSV file
            IDataView newData = mlContext.Data.LoadFromTextFile<DataRFM>(filePath, hasHeader: true, separatorChar: ',', allowQuoting: true);

            // Convert data to a list for normalization
            var dataPoints = mlContext.Data.CreateEnumerable<DataRFM>(newData, reuseRowObject: false).ToList();

            var normalizedDataPoints = new List<DataRFM>();
            foreach (var dataPoint in dataPoints)
            {
                var normalizedDataPoint = new DataRFM
                {
                    CustomerID = dataPoint.CustomerID,
                    Recency = minMaxScalerRecency(dataPoint.Recency),
                    Frequency = minMaxScalerFrequency(dataPoint.Frequency),
                    Monetary = minMaxScalerMonetary(dataPoint.Monetary)
                };
                normalizedDataPoints.Add(normalizedDataPoint);
            }

            // Make predictions on normalized data
            var predictionEngine = mlContext.Model.CreatePredictionEngine<DataRFM, ClusterPrediction>(trainedModel);

            // Create a new output file to save the predictions
            string outputFilePath = Path.Combine(Path.GetDirectoryName(filePath), "PredictedClusters.csv");
            using (var writer = new StreamWriter(outputFilePath))
            {
                writer.WriteLine("CustomerID,Recency_Normalized,Frequency_Normalized,Monetary_Normalized,ClusterID");

                foreach (var normalizedDataPoint in normalizedDataPoints)
                {
                    var prediction = predictionEngine.Predict(normalizedDataPoint);

                    writer.WriteLine($"{normalizedDataPoint.CustomerID},{normalizedDataPoint.Recency},{normalizedDataPoint.Frequency},{normalizedDataPoint.Monetary},{prediction.ClusterID}");
                }
            }
            MessageBox.Show($"Clustering complete. Results saved to: {outputFilePath}");
        }

        static void AddScatterSeries(PlotModel plotModel, string title, (List<double> X, List<double> Y) data, OxyColor color)
        {
            var scatterSeries = new ScatterSeries { Title = title, MarkerType = MarkerType.Circle, MarkerFill = color };
            for (int i = 0; i < data.X.Count; i++)
            {
                scatterSeries.Points.Add(new ScatterPoint(data.X[i], data.Y[i]));
            }
            plotModel.Series.Add(scatterSeries);
        }

        static void SavePlotModel(PlotModel plotModel, string filePath)
        {
            using (var stream = File.Create(filePath))
            {
                var exporter = new SvgExporter { Width = 1920, Height = 1080 };
                exporter.Export(plotModel, stream);
            }
            Console.WriteLine($"Plot saved as SVG at {filePath}");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            PredictFromCsvFile(tb_file.Text);
        }
    }
}
