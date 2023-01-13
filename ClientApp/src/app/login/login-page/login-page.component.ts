import { QuestionsService } from './../../services/questions.service';
import { Component, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { SSEService } from 'src/app/services/sse.service';

@Component({
  selector: 'app-login-page',
  templateUrl: './login-page.component.html',
  styleUrls: ['./login-page.component.scss']
})
export class LoginPageComponent implements OnInit {

  public memberList=['Marko', 'Bruno','Vedran','Marko2']
  public pin:string=''
  private subs: Subscription=new Subscription();

  constructor(private questionsService: QuestionsService, private sseService:SSEService){ 
    this.subs.add(
      this.questionsService.quiz.subscribe(quiz=>{
        console.log(quiz);
        
        this.pin=quiz.pin;
        this.memberList=quiz.players
      })
    )
  }

  getNewPin(){
    this.questionsService.getNewPin();
  }

  startQuiz(){
   this.questionsService.startMatch()
  }

  test() {
    this.sseService.sendTest();
  }

  ngOnInit(): void {
  }

}
