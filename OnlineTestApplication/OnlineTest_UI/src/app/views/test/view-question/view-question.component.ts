import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-view-question',
  templateUrl: './view-question.component.html',
  styleUrls: ['./view-question.component.scss']
})
export class ViewQuestionComponent implements OnInit {
 
  id:any;
  constructor(private route: ActivatedRoute) { }

  ngOnInit() {
    this.getHero();
  }
  getHero(): void {
    // const id = +this.route.snapshot.paramMap.get('id');
    this.id= +this.route.snapshot.paramMap.get('id');
   // alert(this.id);
    // this.heroService.getHero(id)
    //   .subscribe(hero => this.hero = hero);
  }
}
