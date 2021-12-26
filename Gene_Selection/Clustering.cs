using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gene_Selection
{
    class Clustering
    {
        int numAttributes;
        int numClusters;
        int maxCount;
        int numTuple;
        int Distance_method;

        public int[] Number_Cluster;

        public double[] SE;
        public double SSE;
        public double[,] rawData;
        public int[] Classes;

        public double Davis;
        public double r;
        public double r_index;
        int[] clustering;

        public Clustering(double[,] Data,int [] classes, int Num_Cluster, int Num_Attr, int Num_Tuple, int It, int distance)
        {

            numTuple = Num_Tuple;
            numClusters = Num_Cluster;
            numAttributes = Num_Attr;
            maxCount = It;
            Distance_method = distance;

            Number_Cluster = new int[Num_Cluster];

            rawData = Data;
            Classes = classes;
        }
        //*************************************************************************************************************
        public int[] Run_Clustering()
        {
            bool changed = true;
            int ct = 0;
            int numTuples = numTuple;
            clustering = InitClustering(numTuples, numClusters, 0);
            SE = new double[numTuple];
            double[,] means = new double[numClusters, numAttributes];       // just makes things a bit cleaner
            double[,] centroids = new double[numClusters, numAttributes];
            UpdateMeans(rawData, clustering, means);                       // could call this inside UpdateCentroids instead
            UpdateCentroids(rawData, clustering, means, centroids);

            while (changed == true && ct < maxCount)
            {
                ++ct;
                changed = Assign(rawData, clustering, centroids); // use centroids to update cluster assignment
                UpdateMeans(rawData, clustering, means);  // use new clustering to update cluster means
                UpdateCentroids(rawData, clustering, means, centroids);  // use new means to update centroids
                SSE = 0;
                for (int i = 0; i < numTuple; i++)
                {
                    SE[i] = Distance(i, centroids, clustering[i]);
                    SSE += SE[i];
                }
            }

            // Calc Number Of Cluster
            for (int i = 0; i < numTuple; i++)
            {
                Number_Cluster[clustering[i]]++;
            }

            // End of Calc

            Davis = Davis_Buldin(clustering, centroids);
            r_index = Calc_r(clustering, centroids);
            //reclustering();
            return clustering;
        }
        //*************************************************************************************************************
        public int[] InitClustering(int numTuples, int numClusters, int randomSeed)
        {
            // assign each tuple to a random cluster, making sure that there's at least
            // one tuple assigned to every cluster
            Random random = new Random();
            int[] clustering = new int[numTuples];

            // assign first numClusters tuples to clusters 0..k-1
            List<int> All_Data = new List<int>();
            List<int> C = new List<int>();

            for (int i = 0; i < numTuple; i++)
                All_Data.Add(i);
            int index_c = 0;
            for (int i = 0; i < numClusters; i++)
                clustering[i] = i;

            for (int i = numClusters; i < clustering.Length; ++i)
                clustering[i] = random.Next(numClusters);
            return clustering;
        }
        //*************************************************************************************************************
        public void UpdateMeans(double[,] rawData, int[] clustering, double[,] means)
        {
            // assumes means[][] exists. consider making means[][] a ref parameter
            //int numClusters = means.Length;
            // zero-out means[][]
            for (int k = 0; k < numClusters; ++k)
                for (int j = 0; j < numAttributes; ++j)
                    means[k, j] = 0.0;

            // make an array to hold cluster counts
            int[] clusterCounts = new int[numClusters];

            // walk through each tuple, accumulate sum for each attribute, update cluster count
            for (int i = 0; i < numTuple; ++i)
            {
                int cluster = clustering[i];
                ++clusterCounts[cluster];

                for (int j = 0; j < numAttributes; ++j)
                    means[cluster, j] += rawData[i, j];
            }

            // divide each attribute sum by cluster count to get average (mean)
            for (int k = 0; k < numClusters; ++k)
                for (int j = 0; j < numAttributes; ++j)
                    means[k, j] /= clusterCounts[k];  // will throw if count is 0. consider an error-check

        } // UpdateMeans
        //*************************************************************************************************************
        public void UpdateCentroids(double[,] rawData, int[] clustering, double[,] means, double[,] centroids)
        {
            // updates all centroids by calling helper that updates one centroid
            for (int k = 0; k < numClusters; ++k)
            {
                double[] centroid = ComputeCentroid(rawData, clustering, k, means);
                //centroids[k] = centroid;
                for (int l = 0; l < numAttributes; l++)
                    centroids[k, l] = centroid[l];
            }
        }
        //*************************************************************************************************************
        public double[] ComputeCentroid(double[,] rawData, int[] clustering, int cluster, double[,] means)
        {
            // the centroid is the actual tuple values that are closest to the cluster mean
            double[] centroid = new double[numAttributes];
            double minDist = double.MaxValue;
            for (int i = 0; i < numTuple; ++i) // walk thru each data tuple
            {
                int c = clustering[i];  // if curr tuple isn't in the cluster we're computing for, continue on
                if (c != cluster) continue;

                double currDist = Distance(i, means, cluster);  // call helper
                if (currDist < minDist)
                {
                    minDist = currDist;
                    for (int j = 0; j < centroid.Length; ++j)
                        centroid[j] = rawData[i, j];
                }
            }
            return centroid;
        }
        //*************************************************************************************************************
        public double Distance(int i, double[,] m, int k)
        {
            // Euclidean distance between an actual data tuple and a cluster mean or centroid
            double Distance_Result = 0;
            if (Distance_method == 1)
            {
                double sumSquaredDiffs = 0.0;
                for (int j = 0; j < numAttributes; ++j)
                    sumSquaredDiffs += Math.Pow((rawData[i, j] - m[k, j]), 2);
                Distance_Result = Math.Sqrt(sumSquaredDiffs);
            }
            if (Distance_method == 2)
            {
                double Sigma_AdotB = 0;
                double Sigma_A = 0;
                double Sigma_B = 0;
                for (int j = 0; j < numAttributes; j++)
                {
                    Sigma_A += rawData[i, j] * rawData[i, j];
                    Sigma_B += m[k, j] * m[k, j];
                    Sigma_AdotB += rawData[i, j] * m[k, j];
                }
                Distance_Result = (Math.Sqrt(Sigma_A) * Math.Sqrt(Sigma_B)) / (Sigma_AdotB);
            }
            return Distance_Result;

        }
        //*************************************************************************************************************
        public bool Assign(double[,] rawData, int[] clustering, double[,] centroids)
        {
            // assign each tuple to best cluster (closest to cluster centroid)
            // return true if any new cluster assignment is different from old/curr cluster
            // does not prevent a state where a cluster has no tuples assigned. see article for details
            bool changed = false;

            double[] distances = new double[numClusters]; // distance from curr tuple to each cluster mean
            for (int i = 0; i < numTuple; ++i)      // walk thru each tuple
            {
                for (int k = 0; k < numClusters; ++k)       // compute distances to all centroids
                    distances[k] = Distance(i, centroids, k);

                int newCluster = MinIndex(distances);  // find the index == custerID of closest 
                if (newCluster != clustering[i]) // different cluster assignment?
                {
                    changed = true;
                    clustering[i] = newCluster;
                } // else no change
            }
            return changed; // was there any change in clustering?
        } // Assign
        //************************************************************************************************************
        void reclustering()
        {
            int num0_clustering = 0;
            int num1_clustering = 0;
            int Max_clustering=0;
            for (int i = 0; i < numTuple; i++)
            {
                if (clustering[i] == 0)
                    num0_clustering++;
                else
                    num1_clustering++;
            }
            if (num0_clustering > num1_clustering)
                Max_clustering = 0;
            else
                Max_clustering = 1;


            int num0_class = 0;
            int num1_class = 0;
            int Max_class = 0;
            for (int i = 0; i < numTuple; i++)
            {
                if (Classes[i] == 0)
                    num0_class++;
                else
                    num1_class++;
            }
            if (num0_class > num1_class)
                Max_class = 0;
            else
                Max_class = 1;

            if (Max_class!=Max_clustering)
            {
                for (int i = 0; i < numTuple; i++)
                {
                    Classes[i] = 1 - Classes[i];
                }
            }


        }
        public int MinIndex(double[] distances)
        {
            // index of smallest value in distances[]
            int indexOfMin = 0;
            double smallDist = distances[0];
            for (int k = 0; k < distances.Length; ++k)
            {
                if (distances[k] < smallDist)
                {
                    smallDist = distances[k]; indexOfMin = k;
                }
            }
            return indexOfMin;
        }
        //*************************************************************************************************************
        public double Davis_Buldin(int[] R, double[,] C)
        {
            double[] Si = new double[numClusters];
            double[,] dij = new double[numClusters, numClusters];
            double[] ri = new double[numClusters];
            double r = 0;

            for (int i = 0; i < numClusters; i++)
            {
                double Sigma = 0;
                for (int j = 0; j < numTuple; j++)
                {
                    if (R[j] == i)
                        Sigma += Distance(j, C, i);
                }
                Si[i] = (1.0001 / (double)Number_Cluster[i]) * Sigma;
            }

            for (int i = 0; i < numClusters; i++)
            {
                for (int j = 0; j < numClusters; j++)
                {
                    dij[i, j] = 0;
                    for (int k = 0; k < numAttributes; k++)
                    {
                        dij[i, j] += Math.Pow(C[i, k] - C[j, k], 2);
                    }
                    dij[i, j] = Math.Sqrt(dij[i, j]);
                }
            }

            for (int i = 0; i < numClusters; i++)
            {
                ri[i] = double.MinValue;
                for (int j = 0; j < numClusters; j++)
                {
                    if (i != j)
                    {
                        if ((Si[i] + Si[j]) / dij[i, j] > ri[i])
                            ri[i] = (Si[i] + Si[j]) / dij[i, j];
                    }
                }
                r += ri[i];
            }
            r = (1.0 / (double)numClusters) * r;
            return r;
        }
        //*************************************************************************************************************
        public double Calc_r(int[] R, double[,] C)
        {
            double[] d = new double[numClusters];
            for (int c = 0; c < numClusters; c++)
            {
                for (int i = 0; i < numTuple; i++)
                {
                    if (R[i] == c)
                        d[c] += Distance(i, C, c);
                }
            }

            double sigma_d = 0;
            for (int i = 0; i < numClusters; i++)
                sigma_d += d[i];
            double sigma_dc = 0;
            for (int i = 0; i < numClusters; i++)
            {
                for (int j = 0; j < numClusters; j++)
                {
                    if (i != j)
                    {
                        double dc = 0;
                        for (int k = 0; k < numAttributes; k++)
                        {
                            dc += Math.Pow(C[i, k] - C[j, k], 2);
                        }
                        dc = Math.Sqrt(dc);
                        sigma_dc += dc;
                    }
                }
            }

            sigma_dc /= 2;
            return (sigma_d / sigma_dc);


        }

        public double Com()
        {
            int t = 0;
            for (int i = 0; i < numTuple; i++)
            {
                if (Classes[i]==clustering[i])
                {
                    t++;
                }
            }
            return (double)(t) / (double)(numTuple);
        }

        public double Put()
        {
            double r = 0;
            List<int>[] List_Cluster = new List<int>[numClusters];

            for (int i = 0; i < numClusters; i++)
            {
                List_Cluster[i] = new List<int>();
                List_Cluster[i].Clear();
            }

            for (int i = 0; i < numTuple; i++)
            {
                List_Cluster[clustering[i]].Add(i);
            }
            int sum = 0;

            for (int i = 0; i < numClusters; i++)
            {
                int index_0=0;
                int index_1 = 0;
                
                for (int j = 0; j < List_Cluster[i].Count; j++)
                {
                    if (Classes[List_Cluster[i][j]] == 0)
                        index_0++;
                    else
                        index_1++;
                }

                if (index_0 > index_1)
                    sum += index_0;
                else
                    sum += index_1;
            }


            return (double)(sum)/(double)(numTuple);
        }

    }
}
