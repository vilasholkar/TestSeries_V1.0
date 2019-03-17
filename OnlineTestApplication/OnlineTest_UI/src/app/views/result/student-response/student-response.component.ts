import { Component, OnInit } from '@angular/core';
import { MatPaginator, MatSort, MatTableDataSource } from '@angular/material';
import { HelperService } from '../../../services/helper.service'
import { APIUrl } from "../../../shared/API-end-points";
import { ActivatedRoute } from '@angular/router';
import { StudentResponse } from '../../../models/result';
import { sanitizeSrcset } from '@angular/core/src/sanitization/url_sanitizer';
import { setRootDomAdapter } from '@angular/platform-browser/src/dom/dom_adapter';
@Component({
  selector: 'app-student-response',
  templateUrl: './student-response.component.html',
  styleUrls: ['./student-response.component.scss']
})
export class StudentResponseComponent implements OnInit {
  panelOpenState = false;
  IsBackButton: boolean;
  StudentID: any;
  TestID: any;
  languageName: any='english';
  IsEnglish = true;
  studentResponse:StudentResponse[];
  constructor(
    private route: ActivatedRoute,
    private helperSvc: HelperService) { }

  ngOnInit() {
    sessionStorage.getItem("userRoles") === 'Admin' ? this.IsBackButton = true : this.IsBackButton = false;
    this.StudentID = +this.route.snapshot.paramMap.get('StudentID');
    this.TestID = +this.route.snapshot.paramMap.get('TestID');
     this.GetStudentResponse(this.StudentID, this.TestID);
  }
  GetStudentResponse(StudentID: any, TestID: any) {
    debugger
    this.helperSvc.getService(APIUrl.GetStudentResponse + "?StudentID=" + StudentID + "&TestID=" + TestID)
      .subscribe(res => {
        if (res.Message === 'Success') {
            this.studentResponse = res.Object as StudentResponse[];
            this.studentResponse.forEach(element => {
              element.Image_English=APIUrl.QuestionImageBaseURL+element.Image_English;
              element.Image_Hindi=APIUrl.QuestionImageBaseURL+element.Image_Hindi;
              var temp:any;
              element.OptionID===1?temp ='A':element.OptionID===2?temp ='B':element.OptionID===3?temp ='C':temp ='D';
              element.OptionID=temp;
              element.AnswerID===1?temp ='A':element.AnswerID===2?temp ='B':element.AnswerID===3?temp ='C':element.AnswerID===4?temp ='D':temp =element.AnswerID;
              element.AnswerID=temp;
            });
        }
      }, error => {
        alert('error');
        this.helperSvc.errorHandler(error.error);
        console.log(error);
      });
  }
  changeLanguage(languageName: string) {
    if (languageName === 'english')
      this.IsEnglish = true;
    else
      this.IsEnglish = false;
  }
}
