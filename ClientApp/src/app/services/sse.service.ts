import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { QuestionsService } from './questions.service';

@Injectable({
  providedIn: 'root',
})
export class SSEService {
  baseUrl: string;

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string,private questionService:QuestionsService) {
    this.baseUrl = baseUrl;
    this.initSSE();
  }

  initSSE() {
    let source = new EventSource(this.baseUrl + 'sse/rn-updates');
    source.onmessage = (event: any) => {
      if (event.data === 'keep-alive') return;
      let data = event.data.split(':');
      let type = data[0];
      switch (type) {
        case 'new-player': {
            let quizId = data[1];
            let username = data[3];
            this.questionService.addNewPlayer(quizId,username);
            break;
        }
        case 'answer': {
            let quizId = data[2];
            let username = data[4];
            let answer = data[6];
            // call service to to submit players answer
            break;
        }
        case 'end-quiz': {
            // only for players, ignore this
            break;
        }
        case 'start-quiz': {
            // only for players, ignore this
            break;
        }
        case 'next-question': {
            // only for players, ignore this
            break;
        }            
      }
    
    };
    source.addEventListener('message', (message) => {
      console.log(message);
    });
  }
}
