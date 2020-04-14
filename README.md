1. Database 

1.1
`SQLLocalDB c "WebSiteStore"`
`SQLLocalDB start "WebSiteStore"`

1.2 Initialization

Use the provided script in `SQL\init.sql`

2. How to run the code

You need:
* visual studio 2019 or something similar
* .net core 3.1.3 SDK

3. Assumptions

Architecture and design is about optimizing the code according to assumptions what will be done with it.

3.1 Website description
In the website description there are two things which I would immediately ask about
* Category
* Login

3.1.1 Category:

I have no idea what "vertical" means. I googled it - it seems to be a business related term that I am not familiar with.

This seems like a catalogue field. Catalogues (fields with preset range of values) can be anywhere between dynamic (i.e. expanding in range very often) or static - like the days in the week for example.
They can be managed from a separate API or they can be inserted directly with new data - in our case with a new website we could theoretically immediately add it in a new category by itself. This is dependent on what is the meaning of the catalogues and how they are used throughout the application, which is ultimately defined by what is their business logic.
Do we want to stick to basic categories as much as possible or to have as fine categorization as possible (practically like tagging)?

I could have just added a string field which would describe the category of the web site, but that would then make it hard to filter websites by category as strings are easy to confuse and hard to query by (expensive sql operation unless you have indexes).
That said: I ended up implementing some frankenstein, which has some preset categories for which you do not have to go to the DB to get them cause they are often used and will presumably never go away. But also you can dynamically add a new category while saving the website entry.

3.1.2 Login:

I cringed a bit when I saw this one. I assume the websites will be read many times and and the login data will be used in a small percentage of those queries.
So this is why I decided not to return this data over the api.
I prefer to expose another API which is to send the person the password over to their email.
Also of course I keep it encrypted in the DB so that if someone gets access to the DB they won't (easily) see the sensitive data. 


3.2 Database:

3.2.1 With very fiew entities needing storage I could have gone for NoSQL, but I have never used it

Additional info:

External libraries used:
 - Dapper - mini ORM (I've never used it before and wanted to try it out)
 - Microsoft dependency injection *
 - Microsoft.
 
Patterns used:
 - unit of work + repository. Well ... not quite because we have to save the entities explicitly and cannot save the whole session as is done in Nhibernate and EntityFramework but it's close enough. The gain of explicitly knowing which thing is saved when is actually not a bad thing to have sometimes. I did not see a reason to optimize one way or the other.