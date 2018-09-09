import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import {Quiz} from '../../../views/test/quiz/models'
import {ViewQuestionService} from '../../../services/admin/view-question.service';

@Component({
  selector: 'app-view-question',
  templateUrl: './view-question.component.html',
  styleUrls: ['./view-question.component.scss']
})
export class ViewQuestionComponent implements OnInit {
 
  id:any;
  quizModel : Quiz;
  constructor(private route: ActivatedRoute,private viewQuestionService: ViewQuestionService) { }

  ngOnInit() {
    this.getQuestionsById();
  }
  // getHero(): void {
  //   // const id = +this.route.snapshot.paramMap.get('id');
  //   this.id= +this.route.snapshot.paramMap.get('id');
  //  // alert(this.id);
  //   // this.heroService.getHero(id)
  //   //   .subscribe(hero => this.hero = hero);
  // }
  getQuestionsById(){
    debugger;
    this.id= +this.route.snapshot.paramMap.get('id');
    this.viewQuestionService.GetQuestionsByTestId(this.id as number)
    .subscribe(data => {
      if(data.Message === 'Success')
      this.quizModel = data.Object;
  },error => {
    alert('error');
    });
  }
}
