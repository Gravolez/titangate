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

# Assumptions #

## Login: ##

I am returning passwords from the API which seems like it is required, but normally this would have to be discussed and depending on usage some precautions taken.
For example sending the password through mail or something when needed, but only saving it through the API.

## Category: ##

This seems to be a catalogue. There are different ways to handle it depending on how dynamic or constant the catalogue is.
In my case I chose to handle it separately from the websites and new categories cannot be saved together with a new website.
If I have time I will add a separate API controller for the categories.

## Screenshots: ##

It seems more appropriate that files are saved through a second call to the API.
Also binary data can be stored in DB, but I chose to keep it on the disk. Precautions are taken with splitting the files in max 100 files in a folder cause windows cannot handle too many files in a single folder well.

## Dapper: ##

I wanted to try dapper cause I have never used it before and it was a bit tricky. EntityFramework would have saved me a lot of trouble with the SQL, but you win some you lose some.
And with EntityFramework I could have done a "more proper" unit of work and repository (i.e. pretending that the objects are just collections in memory and we do not really have a db, but that's a fantasy that IRL very rarely can turn out well. I mean ... we have to think about transactions and possible locking of tables and so on even when using proper unit of work and repository pattern ... at least in all real projects I have worked on that have been the case)

Good things is - we can easily replace dapper.

# Additional info for later to transfer to Design.doc #

External libraries used:
 - Dapper - mini ORM (I've never used it before and wanted to try it out)
 - A shitload of other microsoft libraries
 
Patterns used:
 - unit of work + repository. 
 - data adapter
