# ChoirManagement

### Introduction

 This is a personnel management system for a choir that helps assembling and managing lineups for rehearsals and concerts and offers a personalized timetable for each singer.


 ChoirManagementService offers a REST Api backend. API endpoints are defined in swagger. There are also autogenerated CRUD controllers with views for each DB entity which are restricted to admin users.

 There is also a web client in Vue and mobile client in React Native which are still work in progress.

### User roles
 There are 3 levels of users: "user", "manager", "admin".

 User can only interact with their personal Notifications, Concerts, Rehearsals and Projects. They also have readonly access to News.

 Manager can in addition create News, Projects, Rehearsals and Concerts, upload Sheetmusic and invite users to Projects, Rehearsals and Concerts.

 Admin has CRUD access to everything. 

### Basic workflow
 The most basic workflow starts with a manager creating a project. Then they add concerts and rehearsals to the project. After that, the manager selects singers to invite to the project. The singers receive a notification and can see the invitation with a timetable and project info under their "Offers" tab. From there they can respond to the invitation and offer information about their participation in each rehearsal/concert. After that, all concerts and rehearsal under the project will be added to the singers personal timetable and the information about singers participation will be visible alongside all the other singers in a general table visible to the managers.

### ERD Schema:
![ERD](https://user-images.githubusercontent.com/49093021/154834877-d9a04fb4-4d15-43bf-93d0-4ad04ba4a3ae.PNG)