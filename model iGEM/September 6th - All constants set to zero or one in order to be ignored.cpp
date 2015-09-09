
/*#include <iostream>
#include <utility>
#include <type_traits>
#include <typeinfo>
#include <cxxabi.h>
#include <time.h>
#include <stdio.h>      /* printf, NULL */
//#include <stdlib.h>     /* srand, rand */
/*#include <vector>
using namespace std;


int main()
{
    
    const auto M = 5;
    const auto N = 10;
    
    
    
    // allocate (no initializatoin)
    int gut[M][N];
    
    // the proper way to zero-initialize the 2D array
    // gut = new int[M][N]();
    
    // seeding random number generator with value from computer cloack
    srand(time(NULL));
    
    // generating numbers between 1 and 6 randomly
    
    
    for(int r = 0; r < M; r++)
    {
        for(int c = 0; c < N; c++)
        {
            int i = (rand()%500);
            
            if (i == 0)
                gut[r][c] = 1;
            
            else if ((i > 0) && (i <= 9))
                gut[r][c] = 2;
            
            else if ((i > 9) && (i <= 24))
                gut[r][c] = 3;
            
            else if ((i > 24) && (i <= 149))
                gut[r][c] = 4;
            
            else if ((i > 149) && (i <= 359))
                gut[r][c] = 5;
            
            else if ((i > 359)&&(i <= 499))
                gut[r][c] = 6;
        }
        
    }
    
    cout << "t = 0" << endl;
    
    // print gut
    for(int r = 0; r < M; r++)
    {
        for(int c = 0; c < N; c++)
            cout << gut[r][c];
        cout << endl;
    }
    cout << " " << endl;
    
    
    
    // Populations
    int pop1 = 0; // Actinobacteria
    int pop2 = 0; // Fusobacteria
    int pop3 = 0; // Proteobacteria
    int pop4 = 0; // Firimicutes
    int pop5 = 0; // Bacteroidetes
    int pop6 = 0; // Other
    // sum of populations
    int popTotal;
    
    double noChange = 1.0; // the variable on which the change is predicated
    double selfConstant = 1.0; // factor that contributes to stability
    double otherConstant = 1.0; // factor that contributes to change
    double tippingPoint = 1.0; // determines result beyond which cells change or not change
    int self; // identity of species at a particular location
    vector<int> surroundings(8,0); // vector represents the 8 surrounding elements, set to zero
    //vector<double> changePotentials(6,0.0); // each of these six numbers represents a likelihood of an element changing into another
    double changeInto = 1.0; // the constant by which the likelihood of an element changing int another is multiplied for every time such an element is encountered in the surroundings
    double inulin = 0.0;
    double inulinDegradationConstant = 1.0;
    
    //Prokao contents
    double inulinAdditionConstant = 0.0;
    int additionConstant1 = 0;
    int additionConstant2 = 0;
    int additionConstant3 = 0;
    int additionConstant4 = 0;
    int additionConstant5 = 0;
    int additionConstant6 = 0;
    
    // fitness values: strength is the ability to appear, durability is the ability to not disappear. Also, influence of inulin
    double s1 = 1; // Actinobacteria
    double s2 = 1; // Fusobacteria
    double s3 = 1; // Proteobacteria
    double s4 = 1; // Firimicutes
    double s5 = 1; // Bacteroidetes
    double s6 = 1; // Other
    
    double d1 = 1; // Actinobacteria
    double d2 = 1; // Fusobacteria
    double d3 = 1; // Proteobacteria
    double d4 = 1; // Firimicutes
    double d5 = 1; // Bacteroidetes
    double d6 = 1; // Other
    
    double i1 = (1 + inulin); // Actinobacteria
    double i2 = 1; // Fusobacteria
    double i3 = 1; // Proteobacteria
    double i4 = (1 + inulin); // Firimicutes
    double i5 = 1; // Bacteroidetes
    double i6 = 1; // Other
    
    
    // cooperation constants (can later be mafe into functions) - these define whether there is a cooperative or competitive effect between members of the same species inhabiting the gut
    /*
     double c1; //= 1 + (pop1/popTotal);
     double c2; //= 1 + (pop2/popTotal);
     double c3; //= 1 + (pop3/popTotal);
     double c4; //= 1 +  (pop4/popTotal);
     double c5; //= 1 + (pop5/popTotal);
     double c6; //= 1 +  (pop6/popTotal);
     */ // These are defined within the loop in order for prokao to account for the changing pop values
    
    
    // obtain the number of bacteria in each of the initial populations

 /*   for(int r = 0; r < M; r++)
    {
        for(int c = 0; c < N; c++){
            switch (gut[r][c]){
                case 1 :
                    pop1++;
                    break;
                case 2 :
                    pop2++;
                    break;
                case 3 :
                    pop3++;
                    break;
                case 4 :
                    pop4++;
                    break;
                case 5 :
                    pop5++;
                    break;
                case 6 :
                    pop6++;
                    break;
            }}}
    
    popTotal = pop1 + pop2 + pop3 + pop4 + pop5 + pop6;
    
    
    cout << "Actinobacteria: " << pop1 << endl;
    cout << "Fusobacteria: " << pop2 << endl;
    cout << "Proteobacteria: " << pop3 << endl;
    cout << "Firmicutes: " << pop4 << endl;
    cout << "Bacteroidetes: " << pop5 << endl;
    cout << "Other: " << pop6 << endl;
    cout << "Total Population: " << popTotal << endl;
    
    //calculating and printing percentage populations
    
    double percentagePop1 = (double(pop1) / popTotal * 100);
    double percentagePop2 = (double(pop2) / popTotal * 100);
    double percentagePop3 = (double(pop3) / popTotal * 100);
    double percentagePop4 = (double(pop4) / popTotal * 100);
    double percentagePop5 = (double(pop5) / popTotal * 100);
    double percentagePop6 = (double(pop6) / popTotal * 100);
    
    cout << "% Actinobacteria: " << percentagePop1 << endl;
    cout << "% Fusobacteria: " << percentagePop2 << endl;
    cout << "% Proteobacteria: " << percentagePop3 << endl;
    cout << "% Firmicutes: " << percentagePop4 << endl;
    cout << "% Bacteroidetes: " << percentagePop5 << endl;
    cout << "% Other: " << percentagePop6 << endl;
    cout << "  " << endl;
    
    
    
    
    
    auto tempGut = new int[M][N]; //introduce a provisional gut inside the loop so that all change is simultaneous within the gut (following a time series)
    
    for (int i=1; i < 10; i++) {
        
        tempGut = gut;
        
        
        inulin *= inulinDegradationConstant; // first-order degradation of inulin with respect to time
        
        if (i%10 == 3) {               //daily dosing of prokao
            inulin += inulinAdditionConstant;
            pop1 += additionConstant1;
            pop2 += additionConstant2;
            pop3 += additionConstant3;
            pop4 += additionConstant4;
            pop5 += additionConstant5;
            pop6 += additionConstant6;
        }
        int popTotal = pop1 + pop2 + pop3 + pop4 + pop5 + pop6;
        
        
        double c1 = 1 ;//+ (double(pop1)/popTotal);
        double c2 = 1 ;//+ (double(pop2)/popTotal);
        double c3 = 1 ;//+ (double(pop3)/popTotal);
        double c4 = 1 ;//+  (double(pop4)/popTotal);
        double c5 = 1 ;//+ (double(pop5)/popTotal);
        double c6 = 1 ;//+  (double(pop6)/popTotal);
        
        
        for(int r = 0; r < M; r++)
        {
            for(int c = 0; c < N; c++){
                
                self            = tempGut[r][c];
                surroundings[0] = tempGut[r-1][c-1];
                surroundings[1] = tempGut[r-1][c];
                surroundings[2] = tempGut[r-1][c+1];
                surroundings[3] = tempGut[r][c-1];
                surroundings[4] = tempGut[r][c+1];
                surroundings[5] = tempGut[r+1][c-1];
                surroundings[6] = tempGut[r+1][c];
                surroundings[7] = tempGut[r+1][c+1];
                
                noChange = 1.0;
                
                
                if (surroundings[0] == self)
                {noChange *= selfConstant;}
                else
                {noChange *= otherConstant;}
                
                if (surroundings[1] == self)
                {noChange *= selfConstant;}
                else
                {noChange *= otherConstant;}
                
                if (surroundings[2] == self)
                {noChange *= selfConstant;}
                else
                {noChange *= otherConstant;}
                
                if (surroundings[3] == self)
                {noChange *= selfConstant;}
                else
                {noChange *= otherConstant;}
                
                if (surroundings[4] == self)
                {noChange *= selfConstant;}
                else
                {noChange *= otherConstant;}
                
                if (surroundings[5] == self)
                {noChange *= selfConstant;}
                else
                {noChange *= otherConstant;}
                
                if (surroundings[6] == self)
                {noChange *= selfConstant;}
                else
                {noChange *= otherConstant;}
                
                if (surroundings[7] == self)
                {noChange *= selfConstant;}
                else
                {noChange *= otherConstant;}
                
                switch (tempGut[r][c]) {
                    case 1:
                        noChange *= d1;
                        break;
                    case 2:
                        noChange *= d2;
                        break;
                    case 3:
                        noChange *= d3;
                        break;
                    case 4:
                        noChange *= d4;
                        break;
                    case 5:
                        noChange *= d5;
                        break;
                    case 6:
                        noChange *= d6;
                        break;
                }
                
                if (noChange > tippingPoint) {
                    gut[r][c] = tempGut[r][c];
                }
                else {
                    vector<double> changePotentials(6,1.0);
                    
                    
                    for (int s=0; s<8; s++) {
                        switch (surroundings[s]){
                            case 1:
                                changePotentials[0]*= (changeInto * s1 * c1 * i1);
                                break;
                            case 2:
                                changePotentials[1]*= (changeInto * s2 * c2 * i2);
                                break;
                            case 3:
                                changePotentials[2]*= (changeInto * s3 * c3 * i3);
                                break;
                            case 4:
                                changePotentials[3]*= (changeInto * s4 * c4 * i4);
                                break;
                            case 5:
                                changePotentials[4]*= (changeInto * s5 * c5 * i5);
                                break;
                            case 6:
                                changePotentials[5]*= (changeInto * s6 * c6 * i6);
                                break;
                        }
                    }
                    
                    for (int i = 0; i<6; i++) {
                        changePotentials[i] *=100000;
                    }
                    
                    int sumPotentials;
                    sumPotentials= changePotentials[0] + changePotentials[1] +changePotentials[2] +changePotentials[3] +changePotentials[4] +changePotentials[5];
                    int x;
                    x = rand()%sumPotentials;
                    if (x<= changePotentials[0] ){
                        gut[r][c] = 1;
                    }
                    else if (x <= (changePotentials[0]+changePotentials[1])){
                        gut[r][c] = 2;}
                    else if (x <= (changePotentials[0]+changePotentials[1]+changePotentials[2])){
                        gut[r][c] = 3;}
                    else if (x <= (changePotentials[0]+changePotentials[1]+changePotentials[2]+changePotentials[3])){
                        gut[r][c] = 4;}
                    else if (x <= (changePotentials[0]+changePotentials[1]+changePotentials[2]+changePotentials[3]+changePotentials[4])){
                        gut[r][c] = 5;}
                    else if (x <= (changePotentials[0]+changePotentials[1]+changePotentials[2]+changePotentials[3]+changePotentials[4]+changePotentials[5])){
                        gut[r][c] = 6;}
                    
                    
                }
                
            }
            
        }
        
        
        cout << "t = "<< i << endl;
        if (i%3 == 0){
            cout << "prokao is given" << endl; }
        
        for(int r = 0; r < M; r++)
        {
            for(int c = 0; c < N; c++)
                cout << gut[r][c];
            cout << endl;
        }
        
        cout << "  " << endl;
        
        
        // Resetting populations
        pop1 = 0; // Actinobacteria
        pop2 = 0; // Fusobacteria
        pop3 = 0; // Proteobacteria
        pop4 = 0; // Firimicutes
        pop5 = 0; // Bacteroidetes
        pop6 = 0; // Other
        
        
        
        // obtain the number of bacteria in each of the populations at t=i
        
        for(int r = 0; r < M; r++)
        {
            for(int c = 0; c < N; c++){
                switch (gut[r][c]){
                    case 1 :
                        pop1++;
                        break;
                    case 2 :
                        pop2++;
                        break;
                    case 3 :
                        pop3++;
                        break;
                    case 4 :
                        pop4++;
                        break;
                    case 5 :
                        pop5++;
                        break;
                    case 6 :
                        pop6++;
                        break;
                }}}
        // sum of initial populations
        popTotal = pop1 + pop2 + pop3 + pop4 + pop5 + pop6;
        
        cout << "Inulin: " << inulin << endl;
        cout << "Actinobacteria: " << pop1 << endl;
        cout << "Fusobacteria: " << pop2 << endl;
        cout << "Proteobacteria: " << pop3 << endl;
        cout << "Firmicutes: " << pop4 << endl;
        cout << "Bacteroidetes: " << pop5 << endl;
        cout << "Other: " << pop6 << endl;
        cout << "Total Population: " << popTotal << endl;
        
        double percentagePop1 = (double(pop1) / popTotal * 100);
        double percentagePop2 = (double(pop2) / popTotal * 100);
        double percentagePop3 = (double(pop3) / popTotal * 100);
        double percentagePop4 = (double(pop4) / popTotal * 100);
        double percentagePop5 = (double(pop5) / popTotal * 100);
        double percentagePop6 = (double(pop6) / popTotal * 100);
        
        cout << "% Actinobacteria: " << percentagePop1 << endl;
        cout << "% Fusobacteria: " << percentagePop2 << endl;
        cout << "% Proteobacteria: " << percentagePop3 << endl;
        cout << "% Firmicutes: " << percentagePop4 << endl;
        cout << "% Bacteroidetes: " << percentagePop5 << endl;
        cout << "% Other: " << percentagePop6 << endl;
        cout << "  " << endl;
        
    }
    
    return 0;
} */