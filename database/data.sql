-- *****************************************************************************
-- This script contains INSERT statements for populating tables with seed data
-- *****************************************************************************

BEGIN TRANSACTION;

-- INSERT statements go here
-- Email is First and last Initial @te.com   example: Harlan Levine  = HL@te.com
-- Passwords are TechElevator1!

INSERT INTO appUser (name, email, password, userType, salt) VALUES ( 'Harlan Levine', 'HL@te.com', 'h0QzvMm9oNihS+9jN40NbuzdHQ8=','r','TGClx61O7Dk='); 
INSERT INTO appUser (name, email, password, userType, salt) VALUES ( 'Aaron Perkins', 'AP@te.com', 'h0QzvMm9oNihS+9jN40NbuzdHQ8=','r','TGClx61O7Dk='); 
INSERT INTO appUser (name, email, password, userType, salt) VALUES ( 'Subhadra Pullabhatla', 'SP@te.com', 'h0QzvMm9oNihS+9jN40NbuzdHQ8=','r','TGClx61O7Dk='); 
INSERT INTO appUser (name, email, password, userType, salt) VALUES ( 'Radu Fotea', 'RF@te.com', 'h0QzvMm9oNihS+9jN40NbuzdHQ8=','r','TGClx61O7Dk='); 
INSERT INTO appUser (name, email, password, userType, salt) VALUES ( 'City Worker', 'CW@te.com', 'h0QzvMm9oNihS+9jN40NbuzdHQ8=','e','TGClx61O7Dk='); 

INSERT INTO pothole (latitude, longitude, whoReported, whoInspected, picture, reportDate, inspectDate, repairStartDate, repairEndDate, severity, comment)
VALUES				(41.50527, -81.68499,	7,			NULL,			NULL, '2017-04-07', NULL,		NULL,			NULL,			NULL,	NULL);

INSERT INTO pothole (latitude, longitude, whoReported, whoInspected, picture, reportDate, inspectDate, repairStartDate, repairEndDate, severity, comment)
VALUES				(41.5038261, -81.68992,	7,			NULL,			NULL, '2017-04-07', NULL,		NULL,			NULL,			NULL,	NULL);

INSERT INTO pothole (latitude, longitude, whoReported, whoInspected, picture, reportDate, inspectDate, repairStartDate, repairEndDate, severity, comment)
VALUES				(41.5052719, -81.67717,	7,			NULL,			NULL, '2017-04-07', NULL,		NULL,			NULL,			NULL,	NULL);

INSERT INTO pothole (latitude, longitude, whoReported, whoInspected, picture, reportDate, inspectDate, repairStartDate, repairEndDate, severity, comment)
VALUES				(41.5094376, -81.6811752,	7,			NULL,			NULL, '2017-04-07', NULL,		NULL,			NULL,			NULL,	NULL);

INSERT INTO pothole (latitude, longitude, whoReported, whoInspected, picture, reportDate, inspectDate, repairStartDate, repairEndDate, severity, comment)
VALUES				(41.50468, -81.69959,	7,			NULL,			NULL, '2017-04-07', NULL,		NULL,			NULL,			NULL,	NULL);

INSERT INTO pothole (latitude, longitude, whoReported, whoInspected, picture, reportDate, inspectDate, repairStartDate, repairEndDate, severity, comment)
VALUES				(41.5004539, -81.68404,	7,			NULL,			NULL, '2017-04-07', NULL,		NULL,			NULL,			NULL,	NULL);

INSERT INTO pothole (latitude, longitude, whoReported, whoInspected, picture, reportDate, inspectDate, repairStartDate, repairEndDate, severity, comment)
VALUES				(41.5000038, -81.69232,	7,			NULL,			NULL, '2017-04-07', NULL,		NULL,			NULL,			NULL,	NULL);

INSERT INTO pothole (latitude, longitude, whoReported, whoInspected, picture, reportDate, inspectDate, repairStartDate, repairEndDate, severity, comment)
VALUES				(41.5028, -81.66357,	7,			NULL,			NULL, '2017-04-07', NULL,		NULL,			NULL,			NULL,	NULL);

INSERT INTO pothole (latitude, longitude, whoReported, whoInspected, picture, reportDate, inspectDate, repairStartDate, repairEndDate, severity, comment)
VALUES				(41.51077, -81.67545,	7,			NULL,			NULL, '2017-04-07', NULL,		NULL,			NULL,			NULL,	NULL);

INSERT INTO pothole (latitude, longitude, whoReported, whoInspected, picture, reportDate, inspectDate, repairStartDate, repairEndDate, severity, comment)
VALUES				(41.5030861, -81.68936,	7,			NULL,			NULL, '2017-04-07', NULL,		NULL,			NULL,			NULL,	NULL);



COMMIT TRANSACTION;