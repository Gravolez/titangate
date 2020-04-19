# Database #

## LocalDB##
`SQLLocalDB c "WebSiteStore"`
`SQLLocalDB start "WebSiteStore"`

## Initialization ##

Use the provided script in `SQL\init.sql`
You can also use `SQL\init_data.sql`  to ... init the data.

# How to run the code #

You need:
* visual studio 2019 or something similar
* .net core 3.1.3 SDK

setup the proper directories and connection strings in `appsettings.json`

## Postman ##
There is some postman file in the main folder which you can use to play with the api.
You may have to setup proper urls and stuff

# Assumptions #

## Login: ##

I am returning passwords from the API which seems like it is required, but normally this would have to be discussed and depending on usage some precautions taken.
For example sending the password through mail or something when needed, but only saving it through the API.

## Category: ##

This seems to be a catalogue. There are different ways to handle it depending on how dynamic or constant the catalogue is.
In my case I chose to handle it separately from the websites and new categories cannot be saved together with a new website.
I have not implemented this API in this project

## Screenshots: ##

It seems more appropriate that files are saved through a second call to the API.
Also binary data can be stored in DB, but I chose to keep it on the disk. Precautions are taken with splitting the files in max 100 files in a folder cause windows cannot handle too many files in a single folder well.

## Dapper: ##

I wanted to try dapper cause I have never used it before and it was a bit tricky. EntityFramework would have saved me a lot of trouble with the SQL, but you win some you lose some.
And with EntityFramework I could have done a "more proper" unit of work and repository (i.e. pretending that the objects are just collections in memory and we do not really have a db, but that's a fantasy that IRL very rarely can turn out well. I mean ... we have to think about transactions and possible locking of tables and so on even when using proper unit of work and repository pattern ... at least in all real projects I have worked on that have been the case)

Good things is - we can easily replace dapper.

# Feedback on the task #

The task requires people to implement a vertical slice of what will be an application of unknown size. 
It seems to me it requires some knowledge and googling skills about how many basic things are done, but it does not go in depth in any department.
While I had vague idea how many of the things are done, most of the scaffolding of applications and most of the infrastructural things are done only once.
Login, repository pattern, file upload, etc ... - only once. After that people mostly copy what already exists (if it works well) or refine it as new or more requirements come in.
I am not experienced in starting new projects as I have mostly worked on big or medium sized projects. This is why for me it was fun and challenging to do this.
I think I hav a bit of a problem with the assumptions part.
There is no any business logic behind the task. There are no business cases - who or what will use this for and so on - I have no idea.
I had a bit of a problem with that on one hand - it's a small project on the other - I think I am suppose to overengineer it a bit. But also I have no idea what to optimize for, cause there is no purpose to any of this beside testing my skills.

I think giving business knowledge and letting people analyze it and implement it is probably better than giving them an interface to implement and letting them make wild guesses as to what to optimize for. In my opinion a huge part of software development is taking real-life processes and implementing them in code. And I feel like in this task there are no real-life processes in the description of the task. Not even made up ones.

But this is just my take on the things based on wild assumptions how your testing process works :)
For all I know it might be really good.

It was challenging for me and I had fun working on it and it made me think, so in fact it did test my skills :)
