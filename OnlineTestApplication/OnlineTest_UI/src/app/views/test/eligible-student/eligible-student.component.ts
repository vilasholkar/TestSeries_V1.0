import { Component, OnInit } from '@angular/core';
import {EligibleStudentService} from '../../../services/admin/eligible-student.service';
import {EligibleStudent} from '../../../models/test';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-eligible-student',
  templateUrl: './eligible-student.component.html',
  styleUrls: ['./eligible-student.component.scss']
})
export class EligibleStudentComponent implements OnInit {

  eligibleStudent: EligibleStudent;
  id: any;
  constructor(private eligibleStudentService:EligibleStudentService,private route:ActivatedRoute) { }

  ngOnInit() {
    this.getEligibleStudent();
  }
  getEligibleStudent(){
  this.id= +this.route.snapshot.paramMap.get('id');
  this.eligibleStudentService.getEligibleStudent(this.id as number)
  .subscribe(data => {
    if(data.Message === 'Success')
    this.eligibleStudent = data.Object;
  },error =>{
    alert('error');
  })
}
addEligibleStudent(){
  alert('Data Saved Successfully');
}
}
