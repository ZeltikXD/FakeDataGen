# Itransition Task #5

Implement a Web-application for the fake (random) user data generation.

The single app page allows to:
1) select region (at least 3 different, e.g. Poland, USA, Georgia or anything you prefer)
2) specify the number of error per record (two “linked” controls — slider 0..10 + binded number field with max value limit at least 1000)
3) define seed value and [Random] button to generate a random seed

If the user change anything, the table below automatically updates (20 records are generated again).

It's necessary to support infinite scrolling in the table (you show 20 records and if the user scroll down, you add next 10 records below — add new so called "page" = "batch of records").

The table show contain the following fields:
1) Index (1, 2, 3, ...) — no errors here
2) Random identifier — no errors here
3) Name + middle name + last name (in region format)
4) Address (in several possible formats, e.g. city+street+building+appartment or county+city+street+house)
5) Phone (again, it's great to have several formats, e.g. international or local ones)

Language of the names/address as well as phone codes/zip codes should be related to the region. You need to generate random data that looks somehow realistically. So, in Poland — Polish, in USA - English or Spanish, etc.

Optional requirement: add Export to CSV button (generate the number of pages which is displayed to user currently). You have to use ready CSV-formatter (DO NOT concatenate string by hands — e.g. address easily can contain comma and semicolon of anything).