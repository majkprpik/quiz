export interface Quiz {
    id:number;
    pin:string;
    players?:string[];
    questions?:Question[];
    strated?:boolean;
    ended?:boolean;
}

export interface Question{
    id:number;
    questionText:string;
    answers:Answer[];
}

export interface Answer{
    id:number;
    answerText:string;
    answerLetter?:number;
    isCorrect:false
}
