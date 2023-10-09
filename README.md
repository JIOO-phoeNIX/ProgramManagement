# ProgramManagement

My understanding of the task is to create endpoints that are needed for a program creation process.

After cloning the project, ensure to update the CosmosDb.Key in the appsettings.json and appsettings.Development.json to that of your instance and if needed, the CosmosDb.Url. 
Also, if the Logs folder is not created by default, please add a Logs folder in the ProgramManagement.Api project.

Next, run the project using IIS from VS and navigate to the swagger UI https://localhost:44394/swagger/index.html


## API Overview

All responses are in the JSON format:
```
{
  "code": int,
  "status": string,
  "message": string,
  "data": object
}
```
The code contains the response code, status the response status, message a message, and data the response object.

## Endpoints

NOTE: authentication is not included in the project.

1. POST api/Program/addprogram: this is used to CREATE a program. The AddProgramRequest Schema shows the parameters needed to be passed to this endpoint. The required parameters are as stated in the design. 
   
Below is a sample request body:
```
{
  "title": "Introduction to C#",
  "summary": "This is basics introduction to C#",
  "description": "it is time to learn the basics of C#",
  "skills": "C#;.NET;EF Core",
  "benefits": "Will help you learn the basics of C#",
  "criteria": "Must have basic Html knowledge",
  "type": "Course",
  "start": "05/05/2023",
  "open": "01/05/2023",
  "close": "10/05/2023",
  "duration": "2 weeks",
  "location": "Canada",
  "isFullyRemote": true,
  "minQualifications": "Undergraduate",
  "maxNoOfApplication": 200
}
```
Note: the skills can be passed as a ; separated string

The type must be a value from the ProgramType enum. 

Success response will be a 200 HTTP response with JSON Response:
```
{
  "code": 200,
  "status": "success",
  "message": "Successful",
  "data": {
    "id": "1582450e478a4e3d837385b84d1bc067"
  }
}
```

The id will be different in your case. Any other response is a failed response. 


2. GET api/Program/getprogram?id=: the id from the above response can be passed to get the details of the program.

3. PUT api/Program/updateprogram: can be used to update the details of a program

4. POST api/program/addapplicationform: used to add the application flow. Thw AddApplicationFormRequest schema shows the request JSON. From my understanding of the design, the personal information and profile fields with a checkbox are those that need to be tracked.
5. 

Sample request body, for fields that the user didn't check any of the boxes, they can be left out in the request. The questions takes an array of the questions the user added for the different sections:
```
{
  "programId": "1582450e478a4e3d837385b84d1bc067",
  "imageUrl": "https://image.png",
  "phone": {
    "isInternal": true,
    "hide": true
  },
  "idNumber": {
    "isInternal": false,
    "hide": true
  },
  "dob": {
    "isInternal": true,
    "hide": false
  },
  "gender": {
    "isInternal": true,
    "hide": true
  },
  "education": {
    "isMandatory": true,
    "hide": true
  },
  "experience": {
    "isMandatory": false,
    "hide": false
  },
 
  "questions": [
    {
      "type": "Paragraph",
      "section": "PersonalInformation",
      "questionText": "Where are you from"
    },
    {
      "type": "MultipleChoice",
      "section": "Profile",
      "questionText": "What skills are your most familiar with?",
      "choices": "C#;Java;Python;Go;Javascript",
      "enableOtherOption": false,
      "maxChoiceAllowed": 2
    },
    {
      "type": "YesNo",
      "section": "AdditionalQuestion",
      "questionText": "Are you familiar with C#",
      "disqualifyIfNo": true
    }
  ]
}
```
Note: the choices can be passed as a ; separated string

5. GET api/Program/getapplicationform?id=: takes the id of the application form and returns the details if found

6. POST api/Program/addworkflow: used to add the workflow stages

Sample request, the appropriate parameter can be passed depending on the type of stage:
```

{
  "programId": "1582450e478a4e3d837385b84d1bc067",
  "stages": [
    {
      "name": "Initial",
      "type": "Shortlisting"
    },
    {
      "name": "Interview",
      "type": "VideoInterview",
      "videoInterviewQuestion": {
        "question": "What is your name",
        "additionalInformation": "What experience do you have",
        "maxDurationOfVideoInSeconds": 120,
        "deadlineForSubmissionInDays": 2
      }
    }
  ]
}
```

7. GET api/Program/getworkflow?id=: takes the id of the work flow and returns the details if found

8. GET api/Program/summary?programId=: takes the id of the program and returns the details if found





