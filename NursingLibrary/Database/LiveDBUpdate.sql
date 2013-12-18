--This script is added to be executed on Live database only.
--This is a one off instance wherr norming data has been pushed in Live DB directly overwriting a current value
--Created By: Kamal
--Created Date: 01/22/2013

DELETE from Norming where TestID = 211 and id =347
