# LSD Exam Report

#### Written by: Joachim Ellingsgaard Christensen, Xinkai Huang and Christian Philip Ege Østergaard Nielsen

# Prelude
This report serves to cap off the project that we have spent the majority of our time during the Large System Development subject on, that being the Hacker News clone. In this document, you will find three sections. 

The first is a description of our own product and the trials and tribulations that we have gone through to design it and make it function. 

The second involves our interactions with the group that we were assigned to operate for, meaning it primarily revolves around our experience with said group whether good or bad, where we also essentially review the quality of the material they provided us.

The last involves a reflection of the work we have done on the project, both in terms of the actual creation of our product as well as the work we have done as operators.

# Requirements, architecture, design and process
In this part of the report, we discuss the product itself. Or rather, the thoughts that went into the creation of it. These thoughts include the following:

•	What requirements we had set up that it needed to follow in the end and how many of them we had missed. 

•	What kind of developmental process we made use of, as to allow a smooth way to work together.

•	What kind of architecture the various parts of our program implemented, as well as how these parts interact with one another.

•	How we designed the parts of the architecture on paper before we got to the programming stage.

•	Finally, how things turned out once we actually put things into action and the problems that popped up as a result.

## System requirements
The system that we had to develop was a clone of a site called Hacker News. This site can be compared to the more contemporarily used Reddit in that it serves as a hub for posts that can contain different content. This includes links to other sites or locally hosted content, both of which can be commented on by the community as much as they would like. In fact, the direct comparison to Reddit only falls apart when you consider that Hacker News is just one site with one community while Reddit has many smaller communities with an overarching structure.

Explaining the site aside, now we have to define the different parts of Hacker News’ system that we were to duplicate in our own. In this case, three primary functions need to be implemented: The ability to make a user, to post a story whether the content is local or not, and to post a comment on these stories.

Building out from that, first we needed a way for the user to log in so that they could be identified. Then we need a way for the user to view the stories that were posted, as well as the comments corresponding to said stories. An element we tinkered with at this point was making a user page so that the user could see what stories and comments they had posted.

An element that was a part of Hacker News, which was not entirely necessary to implement, was the Rating scheme that applied to users and stories otherwise known as Karma. To comment on posts, they needed a certain amount of Karma which they gained by posting stories, which essentially rewarded them for participating in the community.

All of these elements were necessary for our system to serve as a copy of Hacker News, but one more element still had to be implemented. 
We needed an API that could receive posts from our teacher, as he had set up an automatic posting bot that would send requests to each group, as to populate the systems with stories. This would both test the limits of our setup, and catch any small errors we had made in the code that would potentially break the setup.

## Development process
Because of the design of the clone system, we had to program many small parts. Normally, it would be easy to deal these out in bits and pieces, but because of the specific uses they served, we needed to find a way to keep track of how we were going to develop everything.

To accomplish this, we considered a few types of development processes that would aid us in organizing our workload. The main two were the waterfall model, and scrum.

Waterfall essentially makes your workload single minded. You go from point A to point B without looking back. In terms of the project, this would mean that we would develop the database first, then the backend that would connect to this database, then the front-end that would communicate with the backend to display data, and then finally the API that would receive data from afar. This is a very rigid process and it aims to focus on the goal instead of the issues that can crop up after you ‘finish’ an element of the process, so it could end up causing us trouble later down the line.

Scrum is an offshoot of Agile, and essentially, it serves as a development cycle condensed into smaller cycles. You set aside a certain amount of time, like a week or two, to finish a specific bit of the program. Once the time is up, you reflect on the progress made and iterate on what either has already been made, or move onto a new part of the program. Part of what keeps Scrum under control are a set of roles. The Product Owner who is the actual client, the Scrum Master who serves as the leader of the team itself, and the actual development team itself.

While the project suggested the use of Scrum, in the end we only made partial use of it in the sense that we made use of the cycles that were supposed to keep us iterating on the same product, but we did not make use of any roles, merely communicating between ourselves to ensure that everything was being made in proper time.

## Software architecture
The architecture of the system is different depending on what part of the system is examined, therefore this section is split into three sections, one detailing the Back-end and the database that it worked with, one detailing the Front-end and one detailing the API that was receiving data from the teacher. These three sections are also repeated for the Design and the Implementation parts of this report.

### Back-end/Database
The back-end is not a very complex set of code, merely there to serve a set of different commands and objects that that need to be computed from the database. In fact, the primary use for the back-end is just to send SQL statements to the database so that either the system can receive the requested data, or the data in the statements can be inserted into the database.

The database on the other hand is much more important. It is a MSSQL database hosted on Amazon Web Services. This means that a lot of syntax has to be specific to this type of SQL database over others like MySQL, which is one of the more commonly used types of databases and the one we had the most experience programming with.

### Front-end
In simple words, software architecture is the process of converting software characteristics such as flexibility, scalability, feasibility, reusability, and security into a structured solution that meets the technical and the business expectations. According to this definition, we chose to use ASP .NET MVC Framework to develop our Front-end pages, because it is the most compatible framework with our Back-end.

For the Architecture pattern, we used the MVC pattern. MVC stands for Model-View-Controller and it forms the basis of the applications presentation layer. Below you see a small sketch of the pattern.

![MVC Diagram](https://github.com/stanleyh007/Project_HNClone/blob/master/images%20for%20report/mvc%20diagram.png)

Model takes care of all the data and the core functionality of the applications; View is responsible for displaying information to the user; Controller is responsible for accepting user inputs, interpret them to understand what should be done and act accordingly.

Moreover, we also used Micro-services architecture as the basis for how the system was setup between Back-end and Front-end. By using Amazon Web Service, we host our Front-end on their cloud, then interact and communicate with our API and Database, which is also hosted on the cloud.

### API
The API is designed to take raw input from the simulator, which is structured as JSON data, where it then converts it into parseable C#, which we can then send further along into the database using SQL queries that adapt the data we have received. This is the only real function of the API as it is merely intended to be a receiver, while the backend and frontend work with the data inserted into the database.

## Software Design

### Back-end/Database
Because of the needed parameters, the database was set up in such a manner that it contained three primary tables. 

Firstly, it contained the User table that had the user’s name and password as well as an ID that the other two tables could refer to, plus the user’s Karma for use in commenting situations and a date that showed when exactly the user was created.

Next, there was the Stories table that contained a whole slew of different variables that were needed for a single story to be stored. ID, Name, content, owner ID and name, type of post and URL if one was provided, a score, a publish time as well as a list of comments that belonged on that story.

Finally, there was the Comments table. This table contained all comments posted, with an ID to identify them, an amount of content, IDs that referred to both the owner and the story it belonged to, and the time of creation.

Below, you may see a simple diagram of the database that shows how the tables are interconnected.

![Database Diagram](https://github.com/stanleyh007/Project_HNClone/blob/master/images%20for%20report/database%20diagram.png)

With all of this prep work done on the database, it was prepared to take as many inputs as it possibly could without running into issues.

### Front-end
About the design part of Front-end, we had actually a discussion among the group members. We were trying to figure out the difference between .Net Framework and .Net Core, and if the difference was going to have an impact to our system. The answer was “YES”. We actually did some research about the problem but not enough. We were not aware of this technical concern and we did use a lot of time to fix this problem during the development/implementation phase. 

For the design pattern, we designed our system based on GRASP principle. By using MVC pattern, we were already taking care about some parts of the principles. Model is the "information expert"; View is user interface that reflects changes occurring in Model; and finally, Controller that manipulates Model and has a direct relation to GRASP's controller philosophy.

### API
Because of the requirements that we had to fulfill, we were a little concerned on how to make the API accept requests on strange/custom endpoints, to fix this concern we tried to find out how to construct our own custom endpoints. We found out that to make them work like we needed to add annotations to each method that would denote what endpoint would end up in which method. Essentially, we had to hardwire the connections between methods and endpoints. This was the only real struggle we had with the design of the API.

## Software implementation

### Back-end/Database
In the beginning, the database was actually intended to be handled within the backend as a SQLite database where most of the logic was contained, therefore allowing the system to keep track of everything by its lonesome. Unfortunately, this quickly proved ineffective and we soon swapped to using AWS and the MSSQL-type database instead. 

Implementing the functions to call back and forth between the database and the system turned out to be a bit of a bother as the syntax was unfamiliar to us, and as a result, it took longer than expected for the database to run as desired.

What further compounded the database functioning was the fact that we received the parameters we would be receiving from our teacher halfway through the project, throwing out a lot of the work that had initially been done on the database and forcing us to adapt to the elements that he wanted us to compute. 

The hardest part of changing around the database was making sure that the choices for id fields matched those that we were going to receive. In the end, the database worked as intended, even if it later ended up needing many small tweaks to ensure that it would endure the workload that our teacher forced on it.

### Front-end
The Implementation part of the Front-end was quite simple, except for the technical concerns we talked about above. As a standard .NET MVC web application, we just needed to make sure each controller was acting with the right user input and telling the view to show the right data; and that the model classes held the right data. The biggest problem during the development phase was the implementation differences between .NET Framework and .NET Core as we mentioned above: 

1.	Configuration file (The way that the project is configured was totally different)

2.	Version difference (We were using .NET Core 2.1 which was a redesign for the implementation part. Microsoft recommend using Razor Pages instead of MVC.)

These two issues ate up a lot of our time during the implementation phase of our project, and we ended up using twice the time we originally expected to solve the problem. 

We used SQL-Statements to communicate with our API to get the data from our database. 

### API
At the start, we had overlooked some of the requirements; we had not seen that comments could be attached to other comments as replies. Initially we had only set it up so that a comment could be attached to a story, which caused problems because there were many posts that had a comment as a parent, causing them to be lost. This issue was fixed when we changed the link that denoted a comment’s owner from parentID/postID to parentHanesstID. This meant some of our own structure for the database needed to be scrapped, but it was necessary to fulfill the requirements that the teacher placed on us.

For security reasons we have to check if a user is valid. This was not a problem for most of the project, but having to check every time the simulator tried to post caused a certain amount of strain to be put on the system later on. We had to implement caching on our user check so that most of the strain that the system endured was moved away from the database.

# Maintenance and SLA status
This section of the report is focused on our role as operators for one of the other groups; in particular, there were three points would like to highlight during our time with the system.

•	The initial hand-over where we received documentation about the system and we became acquainted with the elements that ran it

•	The creation of the Service-level agreement and how the issues that had cropped up before then influenced how the agreement was made

•	Finally, the overall experience of maintaining the system, including how reliable the individual members of the other team were concerning our issues.

## Hand-over
The hand-over was a very smooth process, at least by our standards. Following meeting them in person on the day of delivery, we were invited into a group on the chat program Discord, where we were able to chat with them on a regular basis should we have any questions for them. 

Beyond this, we received the IP addresses for every relevant page that they deemed necessary for us, and lastly we received their documentation, which was rather extensive in comparison to our own. 

What we immediately noticed when we were given a few moments to tinker with the system however was the fact that they were using RabbitMQ to deal with their various communications between system and database. This resulted in their overall message retention rate being horribly low; as if they had stalled in the low thousands while everyone else were in the higher ten thousands. This heavily influenced our meeting for the SLA.

## Service-level agreement (SLA)

The SLA itself goes as follows:

•	Uptime = 95%

•	Data Loss = 20% Max 

•	Loading Time = 3 sec max

Because of the issues with RabbitMQ that we had noticed before the meeting, we decided on a generous amount of leeway for the group, as their current setup could likely result in further issues. 

This was because of RabbitMQ having a horrendously small limit of messages that it could process at once, and we already knew that the teacher would ramp up the amount of messages that the systems would receive.

While their system was up for an astounding amount of time, the actual data lost had been massive, so we did what we could to ensure that they not only picked a better course than RabbitMQ for communicating, but also had a safety net in terms of further data loss.

It was because of these issues that we reached an agreement of 95% uptime, 20% max data loss (they were at around 90% data lost when we had our meeting), and a loading speed of 3 seconds max to compensate for any changes to the system that they might perform.

## Maintenance and reliability
The actual maintenance of the system was rather painless. The other team did not run into any other problems after our SLA agreement was put into place, as they quickly changed the underlying architecture of their system to compensate, restoring their numbers to a healthy level and ensuring that they did not have any other problems for the rest of the semester. Despite frequent check-ins to ensure that everything was in order, we never found anything that was wrong with the system, and whenever we needed their assistance with any questions, they were happy to provide an answer.

One issue that we did have with their overall responses were the speed. Whenever we needed to get in contact with them, it often took a day or two before they noticed, and this never improved over the course of the semester.
# Discussion
This last segment is dedicated more to an introspective type of content. It involves a reflection of what we have done so far both as developers and as operators. It also includes an overall reflection of our work as a team, and what lessons we have learned from our time on the project.

## Technical discussion
This semester project has had its general difficulties, not just through the way it was programmed, but also the way that it was organized. We have organized these into two small sections that goes over a few of the most important parts, at least in our understanding.

### The good
Making an entire system and then having to maintain both it while also keeping track of another group’s system was actually a fun experience overall. This simulated what it might be like to work on a system in the job market, and that kind of experience might be worth something later down the line.

While we had issues with the project timeline overall, the fact that we had an entire 8 weeks to work on the project meant that we could afford losing some time in the earlier weeks, giving us a bit of leeway with the amount of experimentation we could do. We just wish that the last two weeks did not fall during and after fall break, as we would have liked to relax during this time instead of working on the project.

### The bad
In general, we believe that the project and the semester as a whole has been grossly mismanaged. Elements that were required, like the parameters we were going to receive from the API were left vague until halfway through the project period, leaving us scrambling to ensure that everything fulfilled the necessary criteria.

Another issue we had with the mismanagement of the project was the way that it was incredibly hands off. Most of the lessons were focused on the theorem, while at least a half hour of each lesson could be reserved to check up on the groups to ensure that everything was on track. 

We desperately would’ve like this, as we had organizing problems of our own near the start which cost us some time, combined with the changes we had to do to our system as a result of faulty technology chosen.

### Group-work reflection and lessons learned
Our group work was a very sporadic kind of teamwork. As individuals, we worked wonderfully together to the point where we had absolutely no problem talking with one another. However, due to communicative issues when we were not sitting right next to one another, it did result in a few missteps along the way.

Overall, the three most important things we learned from this project were thus:

The first was setting up a proper hierarchy within the group, allowing one of us to be the boss that would ensure things were being done on time, so that we would be able to keep a light on the goal ahead of us.

The second was establishing what potential disabilities that would possibly hinder the communication between us at any given point in time. It was only last week from when this report was written that one of our members realized that one of the others was a dyslexic, explaining his short replies over chat programs. If this were known sooner, more effort would have been done to ensure a better form of communication.

The last ties into the documentation that we received from the group that we were assigned to operate for… Make a greater effort to make our own documentation in depth. Our own documentation was merely limited to referring to which person in the group had the reins of a certain area of the system, while we should have made a better effort to diagnose potential errors. This way we could have given better answers in case our own operators needed a certain issue solved. 

# Conclusion
In conclusion, this entire project has been a bit of a whirlwind. We had our problems with both the development and the maintaining of the product, and the way the project was handled by the teachers did not exactly help that. 

Once we had a chance to maintain another system, things got onto a course that was much mellower and allowed us to think about what exactly we had been doing, and what we could improve to ensure that our system would reach greater heights.

Ultimately, we appreciate the chance we had to work on the project, and we wish to take the experience with us to make the next ones even better.
