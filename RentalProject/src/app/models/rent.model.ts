export interface RentModel {
    rensCode: number;         
    customerId: number;      
    carCode: number;          
    beginDate: Date;          
    finishDate: Date;   
    rentGoal: string;        
    
    // שדות אופציונליים (הסימן ? אומר שהם לא חובה)
    Cars?: any;               
    Customers?: any;
}