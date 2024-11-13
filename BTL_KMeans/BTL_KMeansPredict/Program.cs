using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.ML;
using Microsoft.ML.Data;
using OxyPlot.Series;
using OxyPlot;

namespace BTL_KMeansPredict
{
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

    class Program
    {
        static void Main(string[] args)
        {
            // Tạo MLContext
            var mlContext = new MLContext();
            string modelPath = "C:\\Users\\Admin\\source\\repos\\BTL_KMeans\\BTL_KMeans\\Output\\ModelKMeans.zip";

            // Tải mô hình đã huấn luyện
            ITransformer trainedModel = mlContext.Model.Load(modelPath, out var modelSchema);

            // Cho phép người dùng chọn chức năng
            Console.WriteLine("Chon chuc nang:");
            Console.WriteLine("1: Nhap du lieu 1 khach hang va doan cum");
            Console.WriteLine("2: Du doan cum cho du lieu khach hang trong file CSV va minh hoa");
            int choice = int.Parse(Console.ReadLine());



            switch (choice)
            {
                case 1:
                    PredictSingleCustomer(mlContext, trainedModel);
                    break;
                case 2:
                    PredictFromCsvAndPlot(mlContext, trainedModel);
                    break;
                default:
                    Console.WriteLine("Lua chon khong hop le.");
                    break;
            }
        }

        static void PredictSingleCustomer(MLContext mlContext, ITransformer trainedModel)
        {
            // Nhập thông tin khách hàng trong khoảng 0-1
            Console.WriteLine("Nhap thong tin khach hang (gia tri tu 0 den 1):");
            Console.Write("Recency: ");
            float recency = float.Parse(Console.ReadLine());
            Console.Write("Frequency: ");
            float frequency = float.Parse(Console.ReadLine());
            Console.Write("Monetary: ");
            float monetary = float.Parse(Console.ReadLine());

            // Chuẩn bị dữ liệu
            var sampleCustomer = new DataRFM
            {
                Recency = recency,
                Frequency = frequency,
                Monetary = monetary
            };

            var predictionEngine = mlContext.Model.CreatePredictionEngine<DataRFM, ClusterPrediction>(trainedModel);
            var prediction = predictionEngine.Predict(sampleCustomer);

            // Hiển thị cụm dự đoán
            Console.WriteLine($"Khach hang thuoc cum: {prediction.ClusterID}");
        }

        static void PredictFromCsvAndPlot(MLContext mlContext, ITransformer trainedModel)
        {
            string newDataPath = "C:\\Users\\Admin\\source\\repos\\BTL_KMeans\\BTL_KMeans\\Data\\rfmtest2.csv";
            IDataView newData = mlContext.Data.LoadFromTextFile<DataRFM>(newDataPath, hasHeader: true, separatorChar: ',', allowQuoting: true);

            var predictions = trainedModel.Transform(newData);
            var predictedResults = mlContext.Data.CreateEnumerable<ClusterPrediction>(predictions, reuseRowObject: false).ToList();
            var dataPoints = mlContext.Data.CreateEnumerable<DataRFM>(newData, reuseRowObject: false).ToList();

            // Tạo file mới để lưu kết quả dự đoán
            string outputFilePath = "C:\\Users\\Admin\\source\\repos\\BTL_KMeans\\BTL_KMeans\\Output\\PredictedClusters.csv";
            using (var writer = new StreamWriter(outputFilePath))
            {
                // Ghi dòng tiêu đề
                writer.WriteLine("CustomerID,Recency,Frequency,Monetary,ClusterID");

                // Ghi dữ liệu từng khách hàng cùng cụm dự đoán
                for (int i = 0; i < predictedResults.Count; i++)
                {
                    var prediction = predictedResults[i];
                    var dataPoint = dataPoints[i];
                    writer.WriteLine($"{dataPoint.CustomerID},{dataPoint.Recency},{dataPoint.Frequency},{dataPoint.Monetary},{prediction.ClusterID}");
                }
            }

            Console.WriteLine($"Du doan da duoc luu tai: {outputFilePath}");


            var cluster1RF = (new List<double>(), new List<double>());
            var cluster2RF = (new List<double>(), new List<double>());
            var cluster3RF = (new List<double>(), new List<double>());

            var cluster1RM = (new List<double>(), new List<double>());
            var cluster2RM = (new List<double>(), new List<double>());
            var cluster3RM = (new List<double>(), new List<double>());

            for (int i = 0; i < predictedResults.Count; i++)
            {
                var prediction = predictedResults[i];
                var dataPoint = dataPoints[i];

                if (prediction.ClusterID == 1)
                {
                    cluster1RF.Item1.Add(dataPoint.Recency);
                    cluster1RF.Item2.Add(dataPoint.Frequency);
                    cluster1RM.Item1.Add(dataPoint.Recency);
                    cluster1RM.Item2.Add(dataPoint.Monetary);
                }
                else if (prediction.ClusterID == 2)
                {
                    cluster2RF.Item1.Add(dataPoint.Recency);
                    cluster2RF.Item2.Add(dataPoint.Frequency);
                    cluster2RM.Item1.Add(dataPoint.Recency);
                    cluster2RM.Item2.Add(dataPoint.Monetary);
                }
                else if (prediction.ClusterID == 3)
                {
                    cluster3RF.Item1.Add(dataPoint.Recency);
                    cluster3RF.Item2.Add(dataPoint.Frequency);
                    cluster3RM.Item1.Add(dataPoint.Recency);
                    cluster3RM.Item2.Add(dataPoint.Monetary);
                }
            }

            // Vẽ biểu đồ Recency-Frequency
            var rfPlotModel = new PlotModel { Title = "K-Means Clustering (Recency vs Frequency)" };
            AddScatterSeries(rfPlotModel, "Cluster 1", cluster1RF, OxyColors.Red);
            AddScatterSeries(rfPlotModel, "Cluster 2", cluster2RF, OxyColors.Blue);
            AddScatterSeries(rfPlotModel, "Cluster 3", cluster3RF, OxyColors.Green);
            SavePlotModel(rfPlotModel, "C:\\Users\\Admin\\source\\repos\\BTL_KMeans\\BTL_KMeans\\Output\\KMeansPlotPredictRF.svg");

            // Vẽ biểu đồ Recency-Monetary
            var rmPlotModel = new PlotModel { Title = "K-Means Clustering (Recency vs Monetary)" };
            AddScatterSeries(rmPlotModel, "Cluster 1", cluster1RM, OxyColors.Red);
            AddScatterSeries(rmPlotModel, "Cluster 2", cluster2RM, OxyColors.Blue);
            AddScatterSeries(rmPlotModel, "Cluster 3", cluster3RM, OxyColors.Green);
            SavePlotModel(rmPlotModel, "C:\\Users\\Admin\\source\\repos\\BTL_KMeans\\BTL_KMeans\\Output\\KMeansPlotPredictRM.svg");

            Console.WriteLine("Bieu do da duoc luu.");
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
    }
}
