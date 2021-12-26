using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gene_Selection
{
    class Ant_Colony
    {
        double[,] W;
        List<List<int>> Com;
        List<Data> Train_Data;
        double[,] Eta;
        double[] Pheromon;
        int[] Fc;
        int Num_Feature;
        int Num_Community;
        int Iteration;
        int Ant_Number;
        int M;
        int Cycle_Lenght;
        double P;
        double q0;
        int[] Community;
        List<int>[] Com_List;
        double[] Information1;
        double Alfa = 1;
        double Beta = 1;
        double Com_Thereshold;

        public Ant_Colony(double[,] w, List<Data> train, int[] com, int num_com, double[] power, int iteration, int ant_num, int cycle_lenght, int m, double p, double q)
        {
            W = w;
            Train_Data = train;
            Information1 = power;
            Eta = w;
            Num_Feature = Convert.ToInt32(Math.Sqrt(W.Length));
            Pheromon = new double[Num_Feature];
            Fc = new int[Num_Feature];
            Community = com;
            Iteration = iteration;
            Ant_Number = ant_num;
            Cycle_Lenght = cycle_lenght;
            M = m;
            Num_Community = num_com;
            P = p;
            q0 = q;

            Com_List = new List<int>[Num_Community];
        }

        public List<int> Run_Ant()
        {
            Random Rand = new Random();
            List<int> Result = new List<int>();
            Set_Initial_Pheromones();
            for (int i = 0; i < Iteration; i++)
            {
                Reset_F();
                for (int j = 0; j < Ant_Number; j++)
                {
                    List<int> Selected = new List<int>();
                    int Initial_Node = Rand.Next(Num_Feature);
                    Selected.Add(Initial_Node);
                    Fc[Initial_Node]++;
                    for (int k = 0; k < Cycle_Lenght - 1; k++)
                    {
                        int Select = Select_Feature(Selected, 0);
                        Selected.Add(Select);
                        Fc[Select]++;
                    }

                }
                Update_Pheromones();
            }
            return Finla_Select();
        }

        void Reset_F()
        {
            for (int i = 0; i < Num_Feature; i++)
                Fc[i] = 0;
        }

        void Update_Pheromones()
        {
            double Sigma_FC = 0;
            for (int i = 0; i < Num_Feature; i++)
                Sigma_FC += Fc[i];
            for (int i = 0; i < Num_Feature; i++)
                Pheromon[i] = ((1 - P) * Pheromon[i]) + ((double)Fc[i] / Sigma_FC);
        }


        int Select_Feature(List<int> selected, int Type)
        {
            Random Rand = new Random();
            double q = Rand.NextDouble();
            List<int> UnSelected = new List<int>();

            List<double> P = new List<double>();

            for (int i = 0; i < Num_Feature; i++)
                if (!selected.Contains(i))
                    UnSelected.Add(i);

            int Count = UnSelected.Count;
            double[] Information2 = new double[UnSelected.Count];
            double[] Information3 = new double[UnSelected.Count];
            double[] Information4 = new double[UnSelected.Count];

            double Sigma = 0;
            double p = 0; ;
            for (int i = 0; i < Count; i++)
            {

                Information2[i] = Calc_Information2_Inc(selected, UnSelected[i]);
                p = Math.Pow(Pheromon[UnSelected[i]], Alfa) * Math.Pow(Information2[i], Beta)*Math.Pow(Information1[UnSelected[i]],Alfa);


                P.Add(p);
                Sigma += p;
            }


            if (q < q0)
            {
                double Max = P.Max();
                int Max_Index = P.IndexOf(Max);
                return UnSelected[Max_Index];
            }

            else
            {
                double[] P_Cum = new double[Count];
                P_Cum[0] = P[0] / Sigma;
                double[] Cum = new double[Count];
                for (int i = 1; i < Count; i++)
                    P_Cum[i] = P_Cum[i - 1] + (P[i] / Sigma);

                double r = Rand.NextDouble();
                int index = 0;
                for (index = 0; index < Count; index++)
                {
                    if (r < P_Cum[index])
                        break;
                }
                return UnSelected[index];
            }
        }

        List<int> Finla_Select()
        {
            List<double> PH_List = new List<double>();
            List<int> PH_Index = new List<int>();
            List<int> Result = new List<int>();
            double Max;
            int Max_Index;

            for (int i = 0; i < Num_Feature; i++)
                PH_List.Add(Pheromon[i]);

            for (int i = 0; i < Num_Feature; i++)
                PH_Index.Add(i);

            for (int i = 0; i < M; i++)
            {
                Max = PH_List.Max();
                Max_Index = PH_List.IndexOf(Max);
                Result.Add(PH_Index[Max_Index]);
                PH_List.RemoveAt(Max_Index);
                PH_Index.RemoveAt(Max_Index);
            }
            return Result;
        }

        void Set_Initial_Pheromones()
        {
            for (int i = 0; i < Num_Feature; i++)
                Pheromon[i] = 0.2;
        }

        double Calc_Information2_Inc(List<int> Selected, int i)
        {
            int Count = Selected.Count;
            double Sigma_Sim = 0;
            for (int ii = 0; ii < Count; ii++)
                Sigma_Sim += W[i, Selected[ii]]+0.0002;

            return (1 / ((1.0 / Count) * Sigma_Sim));
        }

        void Update_Pheomones_Crit(List<int>[] ant_selected, double[] crit)
        {
            double[] Delta = new double[Num_Feature];
            int[] Num = new int[Num_Feature];

            for (int i = 0; i < Ant_Number; i++)
            {
                for (int j = 0; j < ant_selected[i].Count; j++)
                {
                    //Delta[ant_selected[i][j]] += crit[i] / ant_selected[i].Count;
                    Delta[ant_selected[i][j]] += crit[i];
                }
            }

            double Sigma_Ph = 0;
            for (int i = 0; i < Num_Feature; i++)
                Sigma_Ph += Delta[i];


            List<double> Crit_List = crit.ToList();
            double Max = Crit_List.Max();
            int Max_Index = Crit_List.IndexOf(Max);
            for (int i = 0; i < ant_selected[Max_Index].Count; i++)
            {
                Delta[ant_selected[Max_Index][i]] += 5 * crit[Max_Index];

            }

            for (int i = 0; i < Num_Feature; i++)
            {
                Pheromon[i] = 0.9 * Pheromon[i] + Delta[i];
            }
        }

        public List<int> Select_Top(int n)
        {
            List<double> PH_List = new List<double>();
            List<int> PH_Index = new List<int>();
            List<int> Result = new List<int>();
            double Max;
            int Max_Index;

            for (int i = 0; i < Num_Feature; i++)
                PH_List.Add(Pheromon[i]);

            for (int i = 0; i < Num_Feature; i++)
                PH_Index.Add(i);

            for (int i = 0; i < n; i++)
            {
                Max = PH_List.Max();
                Max_Index = PH_List.IndexOf(Max);
                Result.Add(PH_Index[Max_Index]);
                PH_List.RemoveAt(Max_Index);
                PH_Index.RemoveAt(Max_Index);
            }
            return Result;
        }


    }
}
