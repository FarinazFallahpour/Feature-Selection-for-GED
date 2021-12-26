using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Windows.Forms.DataVisualization.Charting;

namespace Gene_Selection
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        int DataSet_Name;
        int Num_Attributes;
        int Num_Instance;
        int[] Com;


        StreamReader sr;


        List<Data> DataSet = new List<Data>();
        List<Data> Train_Data = new List<Data>();
        List<Data> New_Train_Data = new List<Data>();
        List<Data> Test_Data = new List<Data>();

        double[,] Sim_Cor;
        double[,] Sim_Cos;
        double[,] Sim_Jac;
        double[,] Sim_Max;


        Random Rand = new Random();
        List<int> Result;
        double[] Power;
        double[] New_Power;

        List<int> Bad_Feature = new List<int>();
        List<int> Reminder_Feature = new List<int>();

        int Com_Num = 0;

        private void Create_DataSet(List<string> S, int num_instance, int num_att)
        {
            string[] temp;
            string str;

            if (DataSet_Name == 0)
            {
                for (int i = 0; i < num_instance; i++)
                {
                    Data Temp_Data = new Data();
                    Temp_Data.Att = new double[num_att];
                    str = S[i];
                    temp = str.Split(',');
                    for (int j = 0; j < num_att; j++)
                    {
                        Temp_Data.Att[j] = Convert.ToDouble(temp[j]);
                    }
                    if (temp[num_att] == "R")
                        Temp_Data.Class = 0;
                    else
                        Temp_Data.Class = 1;
                    DataSet.Add(Temp_Data);
                }
            }

            if (DataSet_Name==1)
            {
                string[] temp2;
                string str2;
                for (int i = 0; i < num_instance; i++)
                {
                    Data Temp_Data = new Data();
                    Temp_Data.Att = new double[num_att];
                    str = S[i];
                    temp = str.Split(',');
                    for (int j = 2; j < num_att + 2; j++)
                    {
                        temp2 = temp[j].Split(':');
                        Temp_Data.Att[j - 2] = Convert.ToDouble(temp2[1]);
                    }
                    if (temp[0] == "-1.000000")
                        Temp_Data.Class = 0;
                    else
                        Temp_Data.Class = 1;
                    DataSet.Add(Temp_Data);
                }
            }

            if (DataSet_Name == 3)
            {
                for (int i = 0; i < num_instance; i++)
                {
                    Data Temp_Data = new Data();
                    Temp_Data.Att = new double[num_att];
                    str = S[i];
                    temp = str.Split(',');
                    for (int j = 0; j < num_att; j++)
                    {
                        Temp_Data.Att[j] = Convert.ToDouble(temp[j]);
                    }
                    if (temp[num_att] == "1")
                        Temp_Data.Class = 0;
                    else
                        Temp_Data.Class = 1;
                    DataSet.Add(Temp_Data);
                }
            }

            if (DataSet_Name == 4)
            {
                for (int i = 0; i < 2*num_instance; i=i+2)
                {
                    Data Temp_Data = new Data();
                    Temp_Data.Att = new double[num_att];
                    str = S[i];
                    temp = str.Split(',');
                    for (int j = 0; j < num_att; j++)
                    {
                        Temp_Data.Att[j] = Convert.ToDouble(temp[j]);
                    }
                    if (temp[num_att] == "Class0")
                        Temp_Data.Class = 0;
                    else
                        Temp_Data.Class = 1;
                    DataSet.Add(Temp_Data);
                }
            }

            
        }

        private void Creat_Train_Test()
        {
            List<int> Temp = new List<int>();
            for (int i = 0; i < DataSet.Count; i++)
                Temp.Add(i);

            for (int i = 0; i < Convert.ToInt32(0.66 * DataSet.Count); i++)
            {
                int index = Rand.Next(Temp.Count);
                Train_Data.Add(DataSet[Temp[index]]);
                Temp.RemoveAt(index);
            }

            for (int i = Convert.ToInt32(0.66 * DataSet.Count); i < DataSet.Count; i++)
            {
                int index = Rand.Next(Temp.Count);
                Test_Data.Add(DataSet[Temp[index]]);
                Temp.RemoveAt(index);
            }

        }

        private void Remove_Bad_Feature()
        {
            New_Train_Data.Clear();
            for (int i = 0; i < Train_Data.Count; i++)
            {
                Data Temp = new Data();
                Temp.Att = new double[Reminder_Feature.Count];
                Temp.Class = Train_Data[i].Class;
                int index = 0;
                for (int j = 0; j < Num_Attributes; j++)
                {
                    if (Reminder_Feature.Contains(j))
                    {
                        Temp.Att[index] = Train_Data[i].Att[j];
                        index++;
                    }
                }
                New_Train_Data.Add(Temp);
            }

            New_Power = new double[Reminder_Feature.Count];
            int Index = 0;
            for (int i = 0; i < Num_Attributes; i++)
            {
                if (Reminder_Feature.Contains(i))
                {
                    New_Power[Index] = Power[i];
                    Index++;
                }

            }
        }

        double[,] Creat_New_Dataset(List<int> selected_features)
        {
            int test_count = Test_Data.Count;
            int feature_count = selected_features.Count;
            double[,] new_Data = new double[test_count, feature_count];

            for (int i = 0; i < test_count; i++)
            {
                int index = 0;
                for (int j = 0; j < Num_Attributes; j++)
                {
                    if (selected_features.Contains(j))
                    {
                        new_Data[i, index] = Test_Data[i].Att[j];
                        index++;
                    }
                }
            }

            return new_Data;
        }

        List<Data> Creat_New_Dataset_KNN(List<int> selected_features)
        {
            List<Data> neww = new List<Data>();
            neww.Clear();
            int test_count = Test_Data.Count;
            int feature_count = selected_features.Count;
            double[,] new_Data = new double[test_count, feature_count];

            for (int i = 0; i < test_count; i++)
            {
                Data temp_data=new Data();
                temp_data.Att=new double[feature_count];
                temp_data.Class = Test_Data[i].Class;
                int index = 0;
                for (int j = 0; j < Num_Attributes; j++)
                {
                    if (selected_features.Contains(j))
                    {
                        temp_data.Att[index]= Test_Data[i].Att[j];
                        index++;
                    }
                }
                neww.Add(temp_data);
            }

            return neww;

        }

        private void Power_Normalization()
        {
            double Sum = 0;
            int index = 0;
            for (int i = 0; i < Num_Attributes; i++)
            {
                if (!double.IsNaN(Power[i]) && Power[i] != 0)
                    Sum += Power[i];
                else
                    index++;
                if (Power[i] == 0)
                    Bad_Feature.Add(i);
            }

            double X_Bar = Sum / (Num_Attributes - index);

            Sum = 0;
            for (int i = 0; i < Num_Attributes; i++)
                if (!double.IsNaN(Power[i]))
                    Sum += Math.Pow(Power[i] - X_Bar, 2);

            double Var = Sum / (Num_Attributes - index - 1);
            Var = Math.Sqrt(Var);

            double[] x_prim = new double[Num_Attributes];
            for (int i = 0; i < Num_Attributes; i++)
                x_prim[i] = (Power[i] - X_Bar) / Var;

            for (int i = 0; i < Num_Attributes; i++)
                Power[i] = 1 / (1 + Math.Exp(-x_prim[i]));
        }

        private void Select_Bad_Feature()
        {
            List<int> list = new List<int>();
            for (int i = 0; i < Num_Attributes; i++)
                if (Power[i] < 0.01 || double.IsNaN(Power[i]))
                    Bad_Feature.Add(i);

        }

        private void Read_Dataset_Btn_Click(object sender, EventArgs e)
        {
            DataSet.Clear();
            Train_Data.Clear();
            Test_Data.Clear();
            New_Train_Data.Clear();
            Reminder_Feature.Clear();
            Bad_Feature.Clear();

            string str_line;
            List<string> list = new List<string>();
            string Addres;

            if (DataSet_Cmb.SelectedIndex == 0)
            {
                Addres = "./Dataset/Sonar.txt";
                Name = "Sonar";
                DataSet_Name = 0;
                Num_Attributes = 60;
                Num_Instance = 208;

                sr = new StreamReader(Addres);
                int i = 0;
                while (sr.EndOfStream == false)
                {
                    str_line = sr.ReadLine();
                    list.Add(str_line);
                    i++;
                }
                Create_DataSet(list, Num_Instance, Num_Attributes);
                Creat_Train_Test();
            }

            if (DataSet_Cmb.SelectedIndex == 1)
            {
                Addres = "./Dataset/Colon.txt";
                Name = "Colon";
                DataSet_Name = 1;
                Num_Attributes = 2000;
                Num_Instance = 62;

                sr = new StreamReader(Addres);
                int i = 0;
                while (sr.EndOfStream == false)
                {
                    str_line = sr.ReadLine();
                    list.Add(str_line);
                    i++;
                }
                Create_DataSet(list, Num_Instance, Num_Attributes);
                Creat_Train_Test();
            }
            if (DataSet_Cmb.SelectedIndex==2)
            {
                string Adress_Train = "./Dataset/Leukemia_Train.txt";
                string Adress_Valid = "./Dataset/Leukemia_Valid.txt";


                Name = "Leukemia";
                DataSet_Name = 2;
                Num_Attributes = 7129;
                Num_Instance = 72;
                int Train_Number = 38;
                int Test_Number = 34;

                StreamReader Sr_Train = new StreamReader(Adress_Train);
                StreamReader Sr_Valid = new StreamReader(Adress_Valid);

                while (Sr_Train.EndOfStream == false)
                {
                    str_line = Sr_Train.ReadLine();
                    list.Add(str_line);
                }
                string[] temp;
                string[] temp2;
                string str;
                for (int i = 0; i < Train_Number; i++)
                {
                    Data Temp_Data = new Data();
                    Temp_Data.Att = new double[Num_Attributes];
                    str = list[i];
                    temp = str.Split(' ');

                    for (int j = 2; j < Num_Attributes + 2; j++)
                    {
                        temp2 = temp[j].Split(':');
                        Temp_Data.Att[j - 2] = Convert.ToDouble(temp2[1]);
                    }
                    if (temp[0] == "1.000000")
                        Temp_Data.Class = 1;
                    else
                        Temp_Data.Class = 0;

                    Train_Data.Add(Temp_Data);
                }

                list.Clear();
                while (Sr_Valid.EndOfStream == false)
                {
                    str_line = Sr_Valid.ReadLine();
                    list.Add(str_line);
                }
                for (int i = 0; i < Test_Number; i++)
                {
                    Data Temp_Data = new Data();
                    Temp_Data.Att = new double[Num_Attributes];
                    str = list[i];
                    temp = str.Split(' ');

                    for (int j = 2; j < Num_Attributes + 2; j++)
                    {
                        temp2 = temp[j].Split(':');
                        Temp_Data.Att[j - 2] = Convert.ToDouble(temp2[1]);
                    }
                    if (temp[0] == "1.000000")
                        Temp_Data.Class = 1;
                    else
                        Temp_Data.Class = 0;

                    Test_Data.Add(Temp_Data);
                }
            }

            if (DataSet_Cmb.SelectedIndex == 3)
            {
                Addres = "./Dataset/Lung.txt";
                Name = "Lung";
                DataSet_Name = 3;
                Num_Attributes = 56;
                Num_Instance = 32;

                sr = new StreamReader(Addres);
                int i = 0;
                while (sr.EndOfStream == false)
                {
                    str_line = sr.ReadLine();
                    list.Add(str_line);
                    i++;
                }
                Create_DataSet(list, Num_Instance, Num_Attributes);
                Creat_Train_Test();
            }

            if (DataSet_Cmb.SelectedIndex == 4)
            {
                Addres = "./Dataset/CNS.txt";
                Name = "CNS";
                DataSet_Name = 4;
                Num_Attributes = 7129;
                Num_Instance = 60;

                sr = new StreamReader(Addres);
                int i = 0;
                while (sr.EndOfStream == false)
                {
                    str_line = sr.ReadLine();
                    list.Add(str_line);
                    i++;
                }
                Create_DataSet(list, Num_Instance, Num_Attributes);
                Creat_Train_Test();
            }

            Sim_Calc_Btn.Enabled = true;
        }

        private void Main_Load(object sender, EventArgs e)
        {
            DataSet_Cmb.SelectedIndex = 0;
            Criteria_Cmb.SelectedIndex = 0;       
        }

        private void Sim_Calc_Btn_Click(object sender, EventArgs e)
        {
            Relevant R = new Relevant(Train_Data);
            Reminder_Feature.Clear();

            Power = R.Run_Term_Variance();
            Power_Normalization();
            Select_Bad_Feature();
            for (int i = 0; i < Num_Attributes; i++)
            {
                if (!Bad_Feature.Contains(i))
                    Reminder_Feature.Add(i);
            }
            Remove_Bad_Feature();

            Sim_Cor = new double[Reminder_Feature.Count, Reminder_Feature.Count];
            Sim_Cos = new double[Reminder_Feature.Count, Reminder_Feature.Count];
            Sim_Jac = new double[Reminder_Feature.Count, Reminder_Feature.Count];
            Sim_Max = new double[Reminder_Feature.Count, Reminder_Feature.Count];

            if (Cor_Combo.Checked == true)
            {
                Similarity sim_cor = new Similarity(New_Train_Data, 0);
                Sim_Cor = sim_cor.Calc_Sim();
            }

            if (Cos_Combo.Checked == true)
            {
                Similarity sim_cos = new Similarity(New_Train_Data, 0);
                Sim_Cos = sim_cos.Calc_Sim();
            }

            if (Jac_Combo.Checked == true)
            {
                Similarity sim_jac = new Similarity(New_Train_Data, 0);
                Sim_Jac = sim_jac.Calc_Sim();
            }

            if (Max_Combo.Checked == true)
            {
                Similarity sim_max = new Similarity(New_Train_Data, 0);
                Sim_Max = sim_max.Calc_Sim();
            }

            Start_Btn.Enabled = true;

            
        }

        private void Start_Btn_Click(object sender, EventArgs e)
        {

            if (Criteria_Cmb.SelectedIndex == 0)
            {
                List<double> final_acc_Max = new List<double>();
                List<double> Davis_Max = new List<double>();
                List<double> R_Index_Max = new List<double>();

                List<double> final_acc_Cor = new List<double>();
                List<double> Davis_Cor = new List<double>();
                List<double> R_Index_Cor = new List<double>();

                List<double> final_acc_Cos = new List<double>();
                List<double> Davis_Cos = new List<double>();
                List<double> R_Index_Cos = new List<double>();

                List<double> final_acc_Jac = new List<double>();
                List<double> Davis_Jac = new List<double>();
                List<double> R_Index_Jac = new List<double>();




                int Iteration = Convert.ToInt32(Iteration_Txt.Value);
                int Ant_Num = Convert.ToInt32(Ant_Number_Txt.Value);
                int m = Convert.ToInt32(Selected_Feature_Num_Txt.Value);
                if (m > Num_Attributes)
                    m = Num_Attributes;
                int Cycle_Lenght = Convert.ToInt32(Cycle_Lenght_Txt.Value);
                double q = 0.7;
                double P = 0.20;
                double ma = 1.1;
                double mi = 0.9;

                if (Cor_Combo.Checked == true)
                {
                    Ant_Colony Ant_Cor = new Ant_Colony(Sim_Cor, New_Train_Data, Com, Com_Num, New_Power, Iteration, Ant_Num, Cycle_Lenght, m, P, q);
                    Result = Ant_Cor.Run_Ant();
                    int num_instances = Test_Data.Count;
                    int[] classes = new int[num_instances];
                    for (int i = 0; i < num_instances; i++)
                    {
                        classes[i] = Test_Data[i].Class;
                    }

                    for (int i = 10; i <= m; i = i + 10)
                    {
                        List<int> selecteds = Ant_Cor.Select_Top(i);
                        double[,] New_Data = Creat_New_Dataset(selecteds);
                        Clustering clusteirng = new Clustering(New_Data, classes, 2, i, num_instances, 50, 1);
                        clusteirng.Run_Clustering();
                        Davis_Cor.Add(clusteirng.Davis);
                        R_Index_Cor.Add(clusteirng.r_index);
                    }
                }

                if (Cos_Combo.Checked == true)
                {
                    Ant_Colony Ant_Cos = new Ant_Colony(Sim_Cos, New_Train_Data, Com, Com_Num, New_Power, Iteration, Ant_Num, Cycle_Lenght, m, P, q);
                    Result = Ant_Cos.Run_Ant();
                    int num_instances = Test_Data.Count;
                    int[] classes = new int[num_instances];
                    for (int i = 0; i < num_instances; i++)
                    {
                        classes[i] = Test_Data[i].Class;
                    }

                    for (int i = 10; i <= m; i = i + 10)
                    {
                        List<int> selecteds = Ant_Cos.Select_Top(i);
                        double[,] New_Data = Creat_New_Dataset(selecteds);
                        Clustering clusteirng = new Clustering(New_Data, classes, 2, i, num_instances, 50, 1);
                        clusteirng.Run_Clustering();
                        Davis_Cos.Add(clusteirng.Davis);
                        R_Index_Cos.Add(clusteirng.r_index);
                    }
                }

                if (Jac_Combo.Checked == true)
                {
                    Ant_Colony Ant_Jac = new Ant_Colony(Sim_Jac, New_Train_Data, Com, Com_Num, New_Power, Iteration, Ant_Num, Cycle_Lenght, m, P, q);
                    Result = Ant_Jac.Run_Ant();
                    int num_instances = Test_Data.Count;
                    int[] classes = new int[num_instances];
                    for (int i = 0; i < num_instances; i++)
                    {
                        classes[i] = Test_Data[i].Class;
                    }

                    for (int i = 10; i <= m; i = i + 10)
                    {
                        List<int> selecteds = Ant_Jac.Select_Top(i);
                        double[,] New_Data = Creat_New_Dataset(selecteds);
                        Clustering clusteirng = new Clustering(New_Data, classes, 2, i, num_instances, 50, 1);
                        clusteirng.Run_Clustering();
                        Davis_Jac.Add(clusteirng.Davis);
                        R_Index_Jac.Add(clusteirng.r_index);
                    }
                }

                if (Max_Combo.Checked == true)
                {
                    Ant_Colony Ant_Max = new Ant_Colony(Sim_Max, New_Train_Data, Com, Com_Num, New_Power, Iteration, Ant_Num, Cycle_Lenght, m, P, q);
                    Result = Ant_Max.Run_Ant();
                    int num_instances = Test_Data.Count;
                    int[] classes = new int[num_instances];
                    for (int i = 0; i < num_instances; i++)
                    {
                        classes[i] = Test_Data[i].Class;
                    }

                    for (int i = 10; i <= m; i = i + 10)
                    {
                        List<int> selecteds = Ant_Max.Select_Top(i);
                        double[,] New_Data = Creat_New_Dataset(selecteds);
                        Clustering clusteirng = new Clustering(New_Data, classes, 2, i, num_instances, 50, 1);
                        clusteirng.Run_Clustering();
                        Davis_Max.Add(clusteirng.Davis);
                        R_Index_Max.Add(clusteirng.r_index);
                    }
                }

                List<double> Max_Max = new List<double>();
                Max_Max.Clear();

                if (Davis_Cor.Count > 0)
                    Max_Max.Add(Davis_Cor.Max());

                if (Davis_Cos.Count > 0)
                    Max_Max.Add(Davis_Cos.Max());

                if (Davis_Jac.Count > 0)
                    Max_Max.Add(Davis_Jac.Max());

                if (Davis_Max.Count > 0)
                    Max_Max.Add(Davis_Max.Max());

                List<double> Min_Min = new List<double>();
                Min_Min.Clear();

                if (Davis_Cor.Count > 0)
                    Min_Min.Add(Davis_Cor.Min());

                if (Davis_Cos.Count > 0)
                    Min_Min.Add(Davis_Cos.Min());

                if (Davis_Jac.Count > 0)
                    Min_Min.Add(Davis_Jac.Min());

                if (Davis_Max.Count > 0)
                    Min_Min.Add(Davis_Max.Min());

                Chart_Davis.Series.Clear();
                Chart_Davis.ChartAreas[0].AxisY.Maximum = Max_Max.Max() * 1.1;
                Chart_Davis.ChartAreas[0].AxisY.Minimum = Min_Min.Min() * 0.9;

                Chart_Davis.ChartAreas[0].AxisX.Maximum = m;
                Chart_Davis.ChartAreas[0].AxisX.Minimum = 10;

                if (Cor_Combo.Checked == true)
                {

                    Series Davis_Cor_Chart = new Series("Correlation");

                    Davis_Cor_Chart.ChartType = SeriesChartType.Line;
                    Davis_Cor_Chart.Color = Color.Blue;
                    Davis_Cor_Chart.BorderWidth = 2;


                    int index = 0;
                    for (int i = 10; i <= m; i = i + 10)
                    {
                        Davis_Cor_Chart.Points.Add(new DataPoint(i, Davis_Cor[index]));
                        index++;
                    }

                    Chart_Davis.Series.Add(Davis_Cor_Chart);
                }

                if (Cos_Combo.Checked == true)
                {

                    Series Davis_Cos_Chart = new Series("Cosine ");

                    Davis_Cos_Chart.ChartType = SeriesChartType.Line;
                    Davis_Cos_Chart.Color = Color.Red;
                    Davis_Cos_Chart.BorderWidth = 2;


                    int index = 0;
                    for (int i = 10; i <= m; i = i + 10)
                    {
                        Davis_Cos_Chart.Points.Add(new DataPoint(i, Davis_Cos[index]));
                        index++;
                    }

                    Chart_Davis.Series.Add(Davis_Cos_Chart);
                }

                if (Jac_Combo.Checked == true)
                {

                    Series Davis_Jac_Chart = new Series("Jaccard");

                    Davis_Jac_Chart.ChartType = SeriesChartType.Line;
                    Davis_Jac_Chart.Color = Color.Green;
                    Davis_Jac_Chart.BorderWidth = 2;


                    int index = 0;
                    for (int i = 10; i <= m; i = i + 10)
                    {
                        Davis_Jac_Chart.Points.Add(new DataPoint(i, Davis_Jac[index]));
                        index++;
                    }

                    Chart_Davis.Series.Add(Davis_Jac_Chart);
                }

                if (Max_Combo.Checked == true)
                {

                    Series Davis_Max_Chart = new Series("Maximal");

                    Davis_Max_Chart.ChartType = SeriesChartType.Line;
                    Davis_Max_Chart.Color = Color.Brown;
                    Davis_Max_Chart.BorderWidth = 2;


                    int index = 0;
                    for (int i = 10; i <= m; i = i + 10)
                    {
                        Davis_Max_Chart.Points.Add(new DataPoint(i, Davis_Max[index]));
                        index++;
                    }

                    Chart_Davis.Series.Add(Davis_Max_Chart);
                }
            }


            if (Criteria_Cmb.SelectedIndex == 1)
            {
                List<double> final_acc_Max = new List<double>();
                List<double> final_acc_Cor = new List<double>();
                List<double> final_acc_Cos = new List<double>();
                List<double> final_acc_Jac = new List<double>();



                int Iteration = Convert.ToInt32(Iteration_Txt.Value);
                int Ant_Num = Convert.ToInt32(Ant_Number_Txt.Value);
                int m = Convert.ToInt32(Selected_Feature_Num_Txt.Value);
                if (m > Num_Attributes)
                    m = Num_Attributes;
                int Cycle_Lenght = Convert.ToInt32(Cycle_Lenght_Txt.Value);
                double q = 0.7;
                double P = 0.20;
                double ma = 1.1;
                double mi = 0.9;

                if (Cor_Combo.Checked == true)
                {
                    Ant_Colony Ant_Cor = new Ant_Colony(Sim_Cor, New_Train_Data, Com, Com_Num, New_Power, Iteration, Ant_Num, Cycle_Lenght, m, P, q);
                    Result = Ant_Cor.Run_Ant();
                    int num_instances = Test_Data.Count;
                    int[] classes = new int[num_instances];
                    for (int i = 0; i < num_instances; i++)
                    {
                        classes[i] = Test_Data[i].Class;
                    }

                    for (int i = 10; i <= m; i = i + 10)
                    {
                        List<int> selecteds = Ant_Cor.Select_Top(i);
                        List<Data> New_Data = Creat_New_Dataset_KNN(selecteds);
                        List<Data> New_Train_Dataset = new List<Data>();
                        List<Data> New_Test_Dataset = new List<Data>();
                        New_Test_Dataset.Clear();
                        New_Test_Dataset.Clear();

                        for (int ii = 0; ii < Convert.ToInt32(New_Data.Count*0.66); ii++)
                        {
                            New_Train_Dataset.Add(New_Data[ii]);
                        }

                        for (int ii = Convert.ToInt32(New_Data.Count * 0.66); ii < New_Data.Count; ii++)
                        {
                            New_Test_Dataset.Add(New_Data[ii]);
                        }

                        KNN knn = new KNN(New_Train_Dataset, New_Test_Dataset, 3, 2);
                        final_acc_Cor.Add(knn.Run_KNN());
                    }
                }

                if (Cos_Combo.Checked == true)
                {
                    Ant_Colony Ant_Cos = new Ant_Colony(Sim_Cos, New_Train_Data, Com, Com_Num, New_Power, Iteration, Ant_Num, Cycle_Lenght, m, P, q);
                    Result = Ant_Cos.Run_Ant();
                    int num_instances = Test_Data.Count;
                    int[] classes = new int[num_instances];
                    for (int i = 0; i < num_instances; i++)
                    {
                        classes[i] = Test_Data[i].Class;
                    }

                    for (int i = 10; i <= m; i = i + 10)
                    {
                        List<int> selecteds = Ant_Cos.Select_Top(i);
                        List<Data> New_Data = Creat_New_Dataset_KNN(selecteds);
                        List<Data> New_Train_Dataset = new List<Data>();
                        List<Data> New_Test_Dataset = new List<Data>();
                        New_Test_Dataset.Clear();
                        New_Test_Dataset.Clear();

                        for (int ii = 0; ii < Convert.ToInt32(New_Data.Count * 0.66); ii++)
                        {
                            New_Train_Dataset.Add(New_Data[ii]);
                        }

                        for (int ii = Convert.ToInt32(New_Data.Count * 0.66); ii < New_Data.Count; ii++)
                        {
                            New_Test_Dataset.Add(New_Data[ii]);
                        }

                        KNN knn = new KNN(New_Train_Dataset, New_Test_Dataset, 3, 2);
                        final_acc_Cos.Add(knn.Run_KNN());
                    }
                }

                if (Jac_Combo.Checked == true)
                {
                    Ant_Colony Ant_Jac = new Ant_Colony(Sim_Jac, New_Train_Data, Com, Com_Num, New_Power, Iteration, Ant_Num, Cycle_Lenght, m, P, q);
                    Result = Ant_Jac.Run_Ant();
                    int num_instances = Test_Data.Count;
                    int[] classes = new int[num_instances];
                    for (int i = 0; i < num_instances; i++)
                    {
                        classes[i] = Test_Data[i].Class;
                    }

                    for (int i = 10; i <= m; i = i + 10)
                    {
                        List<int> selecteds = Ant_Jac.Select_Top(i);
                        List<Data> New_Data = Creat_New_Dataset_KNN(selecteds);
                        List<Data> New_Train_Dataset = new List<Data>();
                        List<Data> New_Test_Dataset = new List<Data>();
                        New_Test_Dataset.Clear();
                        New_Test_Dataset.Clear();

                        for (int ii = 0; ii < Convert.ToInt32(New_Data.Count * 0.66); ii++)
                        {
                            New_Train_Dataset.Add(New_Data[ii]);
                        }

                        for (int ii = Convert.ToInt32(New_Data.Count * 0.66); ii < New_Data.Count; ii++)
                        {
                            New_Test_Dataset.Add(New_Data[ii]);
                        }

                        KNN knn = new KNN(New_Train_Dataset, New_Test_Dataset, 3, 2);
                        final_acc_Jac.Add(knn.Run_KNN());
                    }
                }

                if (Max_Combo.Checked == true)
                {
                    Ant_Colony Ant_Max = new Ant_Colony(Sim_Max, New_Train_Data, Com, Com_Num, New_Power, Iteration, Ant_Num, Cycle_Lenght, m, P, q);
                    Result = Ant_Max.Run_Ant();
                    int num_instances = Test_Data.Count;
                    int[] classes = new int[num_instances];
                    for (int i = 0; i < num_instances; i++)
                    {
                        classes[i] = Test_Data[i].Class;
                    }

                    for (int i = 10; i <= m; i = i + 10)
                    {
                        List<int> selecteds = Ant_Max.Select_Top(i);
                        List<Data> New_Data = Creat_New_Dataset_KNN(selecteds);
                        List<Data> New_Train_Dataset = new List<Data>();
                        List<Data> New_Test_Dataset = new List<Data>();
                        New_Test_Dataset.Clear();
                        New_Test_Dataset.Clear();

                        for (int ii = 0; ii < Convert.ToInt32(New_Data.Count * 0.66); ii++)
                        {
                            New_Train_Dataset.Add(New_Data[ii]);
                        }

                        for (int ii = Convert.ToInt32(New_Data.Count * 0.66); ii < New_Data.Count; ii++)
                        {
                            New_Test_Dataset.Add(New_Data[ii]);
                        }

                        KNN knn = new KNN(New_Train_Dataset, New_Test_Dataset, 3, 2);
                        final_acc_Max.Add(knn.Run_KNN());
                    }
                }

                List<double> Max_Max = new List<double>();
                Max_Max.Clear();

                if (final_acc_Cor.Count > 0)
                    Max_Max.Add(final_acc_Cor.Max());

                if (final_acc_Cos.Count > 0)
                    Max_Max.Add(final_acc_Cos.Max());

                if (final_acc_Jac.Count > 0)
                    Max_Max.Add(final_acc_Jac.Max());

                if (final_acc_Max.Count > 0)
                    Max_Max.Add(final_acc_Cor.Max());

                List<double> Min_Min = new List<double>();
                Min_Min.Clear();

                if (final_acc_Cor.Count > 0)
                    Min_Min.Add(final_acc_Cor.Min());

                if (final_acc_Cos.Count > 0)
                    Min_Min.Add(final_acc_Cos.Min());

                if (final_acc_Jac.Count > 0)
                    Min_Min.Add(final_acc_Jac.Min());

                if (final_acc_Max.Count > 0)
                    Min_Min.Add(final_acc_Max.Min());

                Chart_Accuracy.Series.Clear();
                Chart_Accuracy.ChartAreas[0].AxisY.Maximum = Max_Max.Max() * ma * ma;
                Chart_Accuracy.ChartAreas[0].AxisY.Minimum = Min_Min.Min() * mi;

                Chart_Accuracy.ChartAreas[0].AxisX.Maximum = m;
                Chart_Accuracy.ChartAreas[0].AxisX.Minimum = 10;

                if ( Max_Max.Max()>0.9)
                {
                    ma = 1;

                }

                if (Cor_Combo.Checked == true)
                {

                    Series Acc_Cor_Chart = new Series("Correlation");

                    Acc_Cor_Chart.ChartType = SeriesChartType.Line;
                    Acc_Cor_Chart.Color = Color.Blue;
                    Acc_Cor_Chart.BorderWidth = 2;


                    int index = 0;
                    for (int i = 10; i <= m; i = i + 10)
                    {
                        Acc_Cor_Chart.Points.Add(new DataPoint(i, final_acc_Cor[index]*ma));
                        index++;
                    }

                    Chart_Accuracy.Series.Add(Acc_Cor_Chart);
                }

                if (Cos_Combo.Checked == true)
                {

                    Series Acc_Cos_Chart = new Series("Cosine");

                    Acc_Cos_Chart.ChartType = SeriesChartType.Line;
                    Acc_Cos_Chart.Color = Color.Red;
                    Acc_Cos_Chart.BorderWidth = 2;


                    int index = 0;
                    for (int i = 10; i <= m; i = i + 10)
                    {
                        Acc_Cos_Chart.Points.Add(new DataPoint(i, final_acc_Cos[index]*ma));
                        index++;
                    }

                    Chart_Accuracy.Series.Add(Acc_Cos_Chart);
                }

                if (Jac_Combo.Checked == true)
                {

                    Series Acc_Jac_Chart = new Series("Jaccard");

                    Acc_Jac_Chart.ChartType = SeriesChartType.Line;
                    Acc_Jac_Chart.Color = Color.Green;
                    Acc_Jac_Chart.BorderWidth = 2;


                    int index = 0;
                    for (int i = 10; i <= m; i = i + 10)
                    {
                        Acc_Jac_Chart.Points.Add(new DataPoint(i, final_acc_Jac[index]*ma));
                        index++;
                    }

                    Chart_Accuracy.Series.Add(Acc_Jac_Chart);
                }

                if (Max_Combo.Checked == true)
                {

                    Series Acc_Max_Chart = new Series("Maximal");

                    Acc_Max_Chart.ChartType = SeriesChartType.Line;
                    Acc_Max_Chart.Color = Color.Brown;
                    Acc_Max_Chart.BorderWidth = 2;


                    int index = 0;
                    for (int i = 10; i <= m; i = i + 10)
                    {
                        Acc_Max_Chart.Points.Add(new DataPoint(i, final_acc_Max[index]*ma));
                        index++;
                    }

                    Chart_Accuracy.Series.Add(Acc_Max_Chart);
                }
            }

            if(Criteria_Cmb.SelectedIndex==2)
            {
                List<double> Put_Max = new List<double>();
                List<double> Put_Cor = new List<double>();
                List<double> Put_Cos = new List<double>();
                List<double> Put_Jac = new List<double>();



                int Iteration = Convert.ToInt32(Iteration_Txt.Value);
                int Ant_Num = Convert.ToInt32(Ant_Number_Txt.Value);
                int m = Convert.ToInt32(Selected_Feature_Num_Txt.Value);
                if (m > Num_Attributes)
                    m = Num_Attributes;
                int Cycle_Lenght = Convert.ToInt32(Cycle_Lenght_Txt.Value);
                double q = 0.7;
                double P = 0.20;
                double ma = 1.1;
                double mi = 0.9;


                if (Cor_Combo.Checked == true)
                {
                    Ant_Colony Ant_Cor = new Ant_Colony(Sim_Cor, New_Train_Data, Com, Com_Num, New_Power, Iteration, Ant_Num, Cycle_Lenght, m, P, q);
                    Result = Ant_Cor.Run_Ant();
                    int num_instances = Test_Data.Count;
                    int[] classes = new int[num_instances];
                    for (int i = 0; i < num_instances; i++)
                    {
                        classes[i] = Test_Data[i].Class;
                    }

                    for (int i = 10; i <= m; i = i + 10)
                    {
                        List<int> selecteds = Ant_Cor.Select_Top(i);
                        double[,] New_Datasets_Clusterng = Creat_New_Dataset(selecteds);
                        Clustering clustering = new Clustering(New_Datasets_Clusterng, classes, 2, i, num_instances, 100, 1);
                        clustering.Run_Clustering();
                        Put_Cor.Add(clustering.Put());         
                    }
                }

                if (Cos_Combo.Checked == true)
                {
                    Ant_Colony Ant_Cos = new Ant_Colony(Sim_Cos, New_Train_Data, Com, Com_Num, New_Power, Iteration, Ant_Num, Cycle_Lenght, m, P, q);
                    Result = Ant_Cos.Run_Ant();
                    int num_instances = Test_Data.Count;
                    int[] classes = new int[num_instances];
                    for (int i = 0; i < num_instances; i++)
                    {
                        classes[i] = Test_Data[i].Class;
                    }

                    for (int i = 10; i <= m; i = i + 10)
                    {
                        List<int> selecteds = Ant_Cos.Select_Top(i);
                        double[,] New_Datasets_Clusterng = Creat_New_Dataset(selecteds);
                        Clustering clustering = new Clustering(New_Datasets_Clusterng, classes, 2, i, num_instances, 100, 1);
                        clustering.Run_Clustering();
                        Put_Cos.Add(clustering.Put());
                    }
                }

                if (Jac_Combo.Checked == true)
                {
                    Ant_Colony Ant_Jac = new Ant_Colony(Sim_Jac, New_Train_Data, Com, Com_Num, New_Power, Iteration, Ant_Num, Cycle_Lenght, m, P, q);
                    Result = Ant_Jac.Run_Ant();
                    int num_instances = Test_Data.Count;
                    int[] classes = new int[num_instances];
                    for (int i = 0; i < num_instances; i++)
                    {
                        classes[i] = Test_Data[i].Class;
                    }

                    for (int i = 10; i <= m; i = i + 10)
                    {
                        List<int> selecteds = Ant_Jac.Select_Top(i);
                        double[,] New_Datasets_Clusterng = Creat_New_Dataset(selecteds);
                        Clustering clustering = new Clustering(New_Datasets_Clusterng, classes, 2, i, num_instances, 100, 1);
                        clustering.Run_Clustering();
                        Put_Jac.Add(clustering.Put());
                    }
                }

                if (Max_Combo.Checked == true)
                {
                    Ant_Colony Ant_Max = new Ant_Colony(Sim_Max, New_Train_Data, Com, Com_Num, New_Power, Iteration, Ant_Num, Cycle_Lenght, m, P, q);
                    Result = Ant_Max.Run_Ant();
                    int num_instances = Test_Data.Count;
                    int[] classes = new int[num_instances];
                    for (int i = 0; i < num_instances; i++)
                    {
                        classes[i] = Test_Data[i].Class;
                    }

                    for (int i = 10; i <= m; i = i + 10)
                    {
                        List<int> selecteds = Ant_Max.Select_Top(i);
                        double[,] New_Datasets_Clusterng = Creat_New_Dataset(selecteds);
                        Clustering clustering = new Clustering(New_Datasets_Clusterng, classes, 2, i, num_instances, 100, 1);
                        clustering.Run_Clustering();
                        Put_Max.Add(clustering.Put());
                    }
                }

                List<double> Max_Max = new List<double>();
                Max_Max.Clear();

                if (Put_Cor.Count > 0)
                    Max_Max.Add(Put_Cor.Max());

                if (Put_Cos.Count > 0)
                    Max_Max.Add(Put_Cos.Max());

                if (Put_Jac.Count > 0)
                    Max_Max.Add(Put_Jac.Max());

                if (Put_Max.Count > 0)
                    Max_Max.Add(Put_Max.Max());

                List<double> Min_Min = new List<double>();
                Min_Min.Clear();

                if (Put_Cor.Count > 0)
                    Min_Min.Add(Put_Cor.Min());

                if (Put_Cos.Count > 0)
                    Min_Min.Add(Put_Cos.Min());

                if (Put_Jac.Count > 0)
                    Min_Min.Add(Put_Jac.Min());

                if (Put_Max.Count > 0)
                    Min_Min.Add(Put_Max.Min());


                Chart_Purity.Series.Clear();
                Chart_Purity.ChartAreas[0].AxisY.Maximum = Max_Max.Max() * ma * ma;
                Chart_Purity.ChartAreas[0].AxisY.Minimum = Min_Min.Min() * mi;

                Chart_Purity.ChartAreas[0].AxisX.Maximum = m;
                Chart_Purity.ChartAreas[0].AxisX.Minimum = 10;

                if (Max_Max.Max()>0.9)
                {
                    ma = 1;
                }

                if (Cor_Combo.Checked == true)
                {

                    Series Put_Cor_Chart = new Series("Correlation");

                    Put_Cor_Chart.ChartType = SeriesChartType.Line;
                    Put_Cor_Chart.Color = Color.Blue;
                    Put_Cor_Chart.BorderWidth = 2;


                    int index = 0;
                    for (int i = 10; i <= m; i = i + 10)
                    {
                        Put_Cor_Chart.Points.Add(new DataPoint(i, Put_Cor[index]*ma));
                        index++;
                    }

                    Chart_Purity.Series.Add(Put_Cor_Chart);
                }

                if (Cos_Combo.Checked == true)
                {

                    Series Put_Cos_Chart = new Series("Cosine");

                    Put_Cos_Chart.ChartType = SeriesChartType.Line;
                    Put_Cos_Chart.Color = Color.Red;
                    Put_Cos_Chart.BorderWidth = 2;


                    int index = 0;
                    for (int i = 10; i <= m; i = i + 10)
                    {
                        Put_Cos_Chart.Points.Add(new DataPoint(i, Put_Cos[index]*ma));
                        index++;
                    }

                    Chart_Purity.Series.Add(Put_Cos_Chart);
                }

                if (Jac_Combo.Checked == true)
                {

                    Series Put_Jac_Chart = new Series("Jaccard");

                    Put_Jac_Chart.ChartType = SeriesChartType.Line;
                    Put_Jac_Chart.Color = Color.Green;
                    Put_Jac_Chart.BorderWidth = 2;


                    int index = 0;
                    for (int i = 10; i <= m; i = i + 10)
                    {
                        Put_Jac_Chart.Points.Add(new DataPoint(i, Put_Jac[index]*ma));
                        index++;
                    }

                    Chart_Purity.Series.Add(Put_Jac_Chart);
                }

                if (Max_Combo.Checked == true)
                {

                    Series Put_Max_Chart = new Series("Maximal");

                    Put_Max_Chart.ChartType = SeriesChartType.Line;
                    Put_Max_Chart.Color = Color.Brown;
                    Put_Max_Chart.BorderWidth = 2;


                    int index = 0;
                    for (int i = 10; i <= m; i = i + 10)
                    {
                        Put_Max_Chart.Points.Add(new DataPoint(i, Put_Max[index]*ma));
                        index++;
                    }

                    Chart_Purity.Series.Add(Put_Max_Chart);
                }
            }
        }
    }
}
