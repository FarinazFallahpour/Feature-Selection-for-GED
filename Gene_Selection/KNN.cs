using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gene_Selection
{
    class KNN
    {
        List<Data> Train_Data;
        List<Data> Valid_Data;
        int K;
        int Num_Class;
        int Num_Feature;

        public KNN(List<Data> train, List<Data> valid, int k, int num_class)
        {
            Train_Data = train;
            Valid_Data = valid;
            K = k;
            Num_Class = num_class;
            Num_Feature = Train_Data[0].Att.Length;
        }

        public double Run_KNN()
        {
            int Valid_Count = Valid_Data.Count;
            int True_Number = 0;
            for (int i = 0; i < Valid_Count; i++)
            {
                if (Find_Class_Neierst(Valid_Data[i]))
                    True_Number++;
            }
            return (double)(True_Number) / (double)(Valid_Count);

        }

        public bool Find_Class_Neierst(Data D)
        {
            int Train_Count = Train_Data.Count;
            List<double> Distance = new List<double>();
            double Sum = 0;
            for (int i = 0; i < Train_Count; i++)
            {
                Sum = 0;
                for (int j = 0; j < Num_Feature; j++)
                {
                    Sum += Math.Pow(D.Att[j] - Train_Data[i].Att[j], 2);
                }
                Distance.Add(Sum);
            }

            double Min;
            int Min_Index;
            List<Data> K_Neirest_List = new List<Data>();
            K_Neirest_List.Clear();
            List<int> Index_List = new List<int>();
            Index_List.Clear();
            for (int i = 0; i < Train_Count; i++)
                Index_List.Add(i);
            for (int i = 0; i < K; i++)
            {
                Min = Distance.Min();
                Min_Index = Distance.IndexOf(Min);
                K_Neirest_List.Add(Train_Data[Index_List[Min_Index]]);
                Index_List.RemoveAt(Min_Index);
                Distance.RemoveAt(Min_Index);
            }

            int[] Class = new int[Num_Class];
            for (int i = 0; i < K; i++)
            {
                Class[K_Neirest_List[i].Class]++;
            }

            int Max_Class = int.MinValue;
            int Max_Class_Index = 0;
            for (int i = 0; i < Num_Class; i++)
            {
                if (Class[i] > Max_Class)
                {
                    Max_Class = Class[i];
                    Max_Class_Index = i;
                }
            }
            if (D.Class == Max_Class_Index)
                return true;
            else
                return false;
        }
    }
}
