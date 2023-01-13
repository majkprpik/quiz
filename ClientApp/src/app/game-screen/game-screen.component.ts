import { QuestionsService } from './../services/questions.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-game-screen',
  templateUrl: './game-screen.component.html',
  styleUrls: ['./game-screen.component.scss'],
})
export class GameScreenComponent implements OnInit {
  constructor(private questionService: QuestionsService) {}

  ngOnInit(): void {
    this.questionService.quiz.subscribe((res) => {
      this.quiz = res;
    });
  }
  quiz;
  currentAnswers;
  currentQuestionNo = 0;

  nextQuestion() {
  }

  showAnswers() {
  }

  addPoints() {
  }


}
