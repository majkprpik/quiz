import { HttpClient } from '@angular/common/http';
import { Injectable, Inject } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { Quiz } from '../models/quiz';

@Injectable({
  providedIn: 'root'
})
export class QuestionsService {

  private baseUrl:string='';
  public quiz:BehaviorSubject<Quiz> = new BehaviorSubject<Quiz>({
    id:0,
    pin:'',
    players:[]
  });

  constructor(private http: HttpClient,@Inject('BASE_URL') baseUrl:string ) { 
    this.baseUrl=baseUrl;
  }

  addNewPlayer(id:number,username:string){

    
    if(this.quiz.value.id===+id){
      let temp=this.quiz.value;
      temp.players.push(username);
      this.quiz.next(temp);
    }
  }

  getNewPin(){
    this.http.get<Quiz>(this.baseUrl +'api/Quiz/NewQuiz').subscribe(res=>{
     
      if(res){
        this.quiz.next(res)
      }else{
        console.log('error');
        
      }
      
    })
  }
  startMatch(){
    this.http.get(this.baseUrl+'/api/Quiz/StartQuiz?quizId='+this.quiz.value.id).subscribe(res=>{
      console.log(res);
      
    })
  }



}
