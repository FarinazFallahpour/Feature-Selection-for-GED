clear all;
close all;
clc;
rand('state',0);

%% Get From Input

disp('================================================================');
disp('1: Iris    |    2: Wine   |     3 :Cancer ');
disp('================================================================');
disp('4: Sonar   |    5: Pima   |     6 :Ionosphere');
disp('================================================================');
disp('7: Leukemia|    8: Colon');
disp('================================================================');

DatasetNum = input('*** Enter Dataset Number :     ');

%% Select Dataset
% Dataset : Dataset with label
% Ds01: standrize [0,1] x-min(X)/max(X)-Min(X)
% ZscoreDs: Dataset normalized with z-score
if DatasetNum<7  
    [Dataset,Ds01,ZscoreDs] = LoadDs(DatasetNum);
    Ds = Dataset(:,1:end-1);
    Label = Dataset(:,end);
    LabelUnq = unique(Label);
    [SampleNum FeatNum]=size(Ds);
end
if DatasetNum==7
    leukidata=load('Leukimia.mat');
    Dataset=leukidata.Ds;
    Ds01=leukidata.Ds(:,1:end);
    %%
    %% Initial Calculation 
    Ds = Dataset(:,1:end-1);
    Label = Dataset(:,end);
    LabelUnq = unique(Label);
    [SampleNum FeatNum]=size(Ds);
end
if DatasetNum==8
    Colon=load('Colon.mat');
    Dataset=Colon.data;
    Dataset=transpose(Dataset);
    Ds01=Colon.data(:,1:end);
    Norm = max(Ds01) - min(Ds01);               % this is a vector
    Norm = repmat(Norm, [length(Ds01) 1]);  % this makes it a matrix of the same size as Sim
    Ds01 = Ds01./Norm;                %  normalized matrix
    Ds01=transpose(Ds01);
    %%
    %% Initial Calculation 
    Ds = Dataset(:,1:end);
    Label = Colon.label;
    LabelUnq = unique(Label);
    [SampleNum FeatNum]=size(Ds);
end

%% Initial Calculation 

%% Binsize
Binsize = 3000; 
%Normalize Dataset
% normData = max(Ds) - min(Ds);               % this is a vector
% normData = repmat(normData, [length(Ds) 1]);  % this makes it a matrix
%                                        % of the same size as A
% Data = Ds./normData;                % your normalized matrix


% Calculate Similarity between Data
Similarity=zeros(FeatNum, FeatNum);

TV=zeros(1,FeatNum);
for q=1:FeatNum
    TV(q)=var(Ds01(:,q));
end
normTV = max(TV) - min(TV);               % this is a vector
normTV = repmat(normTV, [length(TV) 1]);  % this makes it a matrix of the same size as Sim
% TV = TV./normTV;                %  normalized matrix


for i=1:FeatNum
    for j=1:FeatNum
%         Similarity(i,j)= abs(exp(-1.*( sqrt(sum((Ds01(:,i)-Ds01(:,j)))))));
        Similarity(i,j)=sqrt(sum((Ds01(:,i)-Ds01(:,j)).^2));
        
    end
end

%  Calculate Term Variance




%Normalize Similarity
normSim = max(Similarity) - min(Similarity);               % this is a vector
normSim = repmat(normSim, [length(Similarity) 1]);  % this makes it a matrix of the same size as Sim
Similarity = Similarity./normSim;                %  normalized matrix
Similarity=1-Similarity;

AntNum=100;
Iteration=100;
p=0.2;
Cycle=10;
if Cycle>FeatNum
    Cycle=FeatNum;
end

Fc=zeros(1,FeatNum);
Pheromon=zeros(1,FeatNum);

for j=1: FeatNum
      Pheromon(j)=0.2; 
end
   

for i=1:Iteration
    
    for j=1: FeatNum
      Fc(j)=0; 
    end
    
    for A=1:AntNum
        Selected=zeros(1,FeatNum);
        s = round(rand(1)*FeatNum);
        if s==0
            s=1;
        end
        Selected(s)=1;
        Fc(s)=Fc(s)+1;
        
        for c=1:Cycle
            Power=zeros(1,FeatNum);
            
            for j=1:FeatNum
                if Selected(j)==0
                    Power(j)=(1/(Similarity(j,s)+0.01))*Pheromon(j)*TV(j);
                end
            end

                index=0;
                max=0;
                for j=1:FeatNum
                    if Power(j)>max
                        max=Power(j);
                        index=j;
                    end
                end
                s=index;
                if s==0
                    s=1;
                end
                Selected(s)=1;
                Fc(s)=Fc(s)+1;
            
        end
    end
    sum=0;
    for j=1:FeatNum
        sum=sum+Fc(j);
    end
    
    for j=1:FeatNum
        Pheromon(j)=(1-p)*Pheromon(j)+Fc(j)/sum;
    end
end


index=0;
max=0;
FinalSelect=zeros(1,FeatNum);
AllFeatures=zeros(1,FeatNum);
for i=1:FeatNum
  AllFeatures(i)=1;
end

Num=10;
if Num>FeatNum
    Num=FeatNum;
end
for f=1:Num
    index=0;
    max=0;
    for j=1:FeatNum
        if Pheromon(j)>max
            max=Pheromon(j);
            index=j;
        end
    end
    Pheromon(index)=0;
    FinalSelect(index)=1;
end

% ***************
n = 1;
k = size(LabelUnq,1);
[AccDs1,PureDs1,indexw1]=KmeansAccPure(Pheromon,Ds01,Label,n,k);
[AccDs2,PureDs2,indexw2]=KmeansAccPure(AllFeatures,Ds01,Label,n,k);
disp('Feature Rank 0-1 Normalization (With fixBin)-Weight: ent+ - ent -:');
% [s,index]=sort(W21_EntDs01_1(2,:)-W21_EntDs01_1(1,:));
disp(indexw1);
disp('================================================================');
disp('Feature Rank 0-1 Normalization (Without Bin)Weight=Entropy1:');
disp(indexw2);
disp('================================================================');
PlotPureAcc(FeatNum,AccDs1,AccDs2,PureDs1,PureDs2,n);

