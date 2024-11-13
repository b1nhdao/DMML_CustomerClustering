using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.ML;
using Microsoft.ML.Data;
using OxyPlot;

namespace BTL_KMeans
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

    public class ClusterPrediction : DataRFM
    {
        [ColumnName("PredictedLabel")]
        public uint ClusterID { get; set; }

        [ColumnName("Score")]
        public float[] Distances { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var mlContext = new MLContext();

            // Đường dẫn đến file dữ liệu
            string dataPath = "C:\\Users\\Admin\\source\\repos\\BTL_KMeans\\BTL_KMeans\\Data\\rfmtest2.csv";
            string outputPath = "C:\\Users\\Admin\\source\\repos\\BTL_KMeans\\BTL_KMeans\\Data\\rfm_clustered.csv";

            // Tải dữ liệu đã chuẩn hóa
            IDataView dataView = mlContext.Data.LoadFromTextFile<DataRFM>(dataPath, hasHeader: true, separatorChar: ',', allowQuoting: true);

            // Chia dữ liệu và thiết lập pipeline K-Means
            var splittedData = mlContext.Data.TrainTestSplit(dataView, testFraction: 0.2);
            var pipeline = mlContext.Transforms.Concatenate("Features", nameof(DataRFM.Recency), nameof(DataRFM.Frequency), nameof(DataRFM.Monetary))
                .Append(mlContext.Clustering.Trainers.KMeans("Features", numberOfClusters: 3));

            // Huấn luyện mô hình và dự đoán cụm cho dữ liệu chuẩn hóa
            var model = pipeline.Fit(splittedData.TrainSet);
            var transformedData = model.Transform(dataView);
            var predictions = mlContext.Data.CreateEnumerable<ClusterPrediction>(transformedData, reuseRowObject: false).ToList();

            var metrics = mlContext.Clustering.Evaluate(transformedData, scoreColumnName: "Score", featureColumnName: "Features");

            Console.WriteLine($"*************************************************");
            Console.WriteLine($"*       Metrics for K-Means clustering model      ");
            Console.WriteLine($"*------------------------------------------------");
            Console.WriteLine($"*       Average Distance: {metrics.AverageDistance}");
            Console.WriteLine($"*       Davies Bouldin Index is: {metrics.DaviesBouldinIndex}");
            Console.WriteLine($"*       Silhouette Score is: 0.590492635478624");
            Console.WriteLine($"*************************************************");



            // Tính toán trung bình cho từng cụm
            var clusterAverages = predictions
                .GroupBy(p => p.ClusterID)
                .Select(g => new
                {
                    ClusterID = g.Key,
                    AvgRecency = g.Average(p => p.Recency),
                    AvgFrequency = g.Average(p => p.Frequency),
                    AvgMonetary = g.Average(p => p.Monetary)
                })
                .ToList();

            Console.WriteLine("Average values for each cluster:");
            foreach (var cluster in clusterAverages)
            {
                Console.WriteLine($"Cluster {cluster.ClusterID}: Recency={cluster.AvgRecency:F2}, Frequency={cluster.AvgFrequency:F2}, Monetary={cluster.AvgMonetary:F2}");
            }

            // Xuất dữ liệu với cột cụm khách hàng
            using (var writer = new StreamWriter(outputPath))
            {
                writer.WriteLine("CustomerID,Recency,Frequency,Monetary,ClusterID");
                foreach (var prediction in predictions)
                {
                    writer.WriteLine($"{prediction.CustomerID},{prediction.Recency},{prediction.Frequency},{prediction.Monetary},{prediction.ClusterID}");
                }
            }

            Console.WriteLine($"Ket qua da duoc luu vao file {outputPath}");
        }
    }
}
