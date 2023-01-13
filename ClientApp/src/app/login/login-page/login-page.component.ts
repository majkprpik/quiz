import { QuestionsService } from './../../services/questions.service';
import { Component, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-login-page',
  templateUrl: './login-page.component.html',
  styleUrls: ['./login-page.component.scss']
})
export class LoginPageComponent implements OnInit {

  public memberList=['Marko', 'Bruno','Vedran','Marko2']
  public pin:string=''
  private subs: Subscription=new Subscription();

  constructor(private questionsService: QuestionsService) { 
    this.subs.add(
      this.questionsService.quiz.subscribe(quiz=>{
        this.pin=quiz.pin;
        this.memberList=quiz.players
      })
    )
  }

  startQuiz(){
    this.questionsService.startNewQuiz();
  }

  ngOnInit(): void {
  }

}
