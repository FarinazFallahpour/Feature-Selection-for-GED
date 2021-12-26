function [Dataset,Ds01,ZscoreDs] =LoadDs(DatasetNum)

switch DatasetNum
    case 1
       load('iris.mat');
    case 2
       load('wine.mat');
    case 3
     load('cancer.mat');
    case 4
     load('sonar.mat');
    case 5
     load('pima.mat');
   case 6
      load('ionosphere.mat');
   case 7
       load('Leukimia.mat');
      
    otherwise
        disp('Enter Valid Number');
        DatasetNum = input('Enter Dataset Number :');
end