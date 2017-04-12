-- *************************************************************************************************
-- This script creates all of the database objects (tables, constraints, etc) for the database
-- *************************************************************************************************

BEGIN TRANSACTION;

DROP TABLE pothole;
DROP TABLE appUser;

create table appUser(

	userId			int identity(1,1),
	name			varchar(32),
	email			varchar(256) UNIQUE,
	password		varchar(256),
	userType		varchar(1),
	salt			varchar(64),

	constraint pk_user	PRIMARY KEY (userId),
)

create table pothole(

	potholeID		int identity(1,1),
	latitude		real,
	longitude		real,
	whoReported		int,
	whoInspected	int,
	picture			varchar(256),
	reportDate		dateTime,
	inspectDate		dateTime,
	repairStartDate	dateTime,
	repairEndDate	dateTime,
	severity		int,
	comment			varchar(256),

	constraint pk_pothole PRIMARY KEY (potholeID),
	constraint fk_whoReported FOREIGN KEY (whoReported) references appUser (userID),
	constraint fk_whoInspected FOREIGN KEY (whoInspected) references appUser (userID),
)

-- Email is First and last Initial @te.com   example: Harlan Levine  = HL@te.com
-- Passwords are TechElevator1!

INSERT INTO appUser (name, email, password, userType, salt) VALUES ( 'Harlan Levine', 'HL@te.com', 'h0QzvMm9oNihS+9jN40NbuzdHQ8=','r','TGClx61O7Dk='); 
INSERT INTO appUser (name, email, password, userType, salt) VALUES ( 'Aaron Perkins', 'AP@te.com', 'h0QzvMm9oNihS+9jN40NbuzdHQ8=','r','TGClx61O7Dk='); 
INSERT INTO appUser (name, email, password, userType, salt) VALUES ( 'Subhadra Pullabhatla', 'SP@te.com', 'h0QzvMm9oNihS+9jN40NbuzdHQ8=','r','TGClx61O7Dk='); 
INSERT INTO appUser (name, email, password, userType, salt) VALUES ( 'Radu Fotea', 'RF@te.com', 'h0QzvMm9oNihS+9jN40NbuzdHQ8=','r','TGClx61O7Dk='); 
INSERT INTO appUser (name, email, password, userType, salt) VALUES ( 'City Worker', 'CW@te.com', 'h0QzvMm9oNihS+9jN40NbuzdHQ8=','e','TGClx61O7Dk='); 

-- Records more than 180 days old and repaired
INSERT INTO pothole (latitude, longitude, whoReported, whoInspected, picture, reportDate, inspectDate, repairStartDate, repairEndDate, severity, comment)
VALUES				(41.50527, -81.68499,	2,			5,			NULL, '2016-04-07','2016-07-07',	'2016-08-07',   '2016-09-07',		3,		NULL);

INSERT INTO pothole (latitude, longitude, whoReported, whoInspected, picture, reportDate, inspectDate, repairStartDate, repairEndDate, severity, comment)
VALUES				(41.5038261, -81.68992,	3,			5,			NULL, '2016-04-07', '2016-07-07',	'2016-09-07',	'2016-09-15',		5,		NULL);

--Reported and no action taken
INSERT INTO pothole (latitude, longitude, whoReported, whoInspected, picture, reportDate, inspectDate, repairStartDate, repairEndDate, severity, comment)
VALUES				(41.50897, -81.66434,	2,			NULL,			NULL, '2017-03-01', NULL,		NULL,			NULL,			NULL,	NULL);

INSERT INTO pothole (latitude, longitude, whoReported, whoInspected, picture, reportDate, inspectDate, repairStartDate, repairEndDate, severity, comment)
VALUES				(41.5094376, -81.6811752,	4,		NULL,			NULL, '2017-03-03', NULL,		NULL,			NULL,			NULL,	NULL);

INSERT INTO pothole (latitude, longitude, whoReported, whoInspected, picture, reportDate, inspectDate, repairStartDate, repairEndDate, severity, comment)
VALUES				(41.50468, -81.69959,	5,			NULL,			NULL, '2017-03-07', NULL,		NULL,			NULL,			NULL,	NULL);

INSERT INTO pothole (latitude, longitude, whoReported, whoInspected, picture, reportDate, inspectDate, repairStartDate, repairEndDate, severity, comment)
VALUES				(41.5004539, -81.68404,	3,			NULL,			NULL, '2017-03-18', NULL,		NULL,			NULL,			NULL,	NULL);

INSERT INTO pothole (latitude, longitude, whoReported, whoInspected, picture, reportDate, inspectDate, repairStartDate, repairEndDate, severity, comment)
VALUES				(41.5000038, -81.69232,	2,			NULL,			NULL, '2017-03-19', NULL,		NULL,			NULL,			NULL,	NULL);

INSERT INTO pothole (latitude, longitude, whoReported, whoInspected, picture, reportDate, inspectDate, repairStartDate, repairEndDate, severity, comment)
VALUES				(41.5028, -81.66357,	2,			NULL,			NULL, '2017-03-22', NULL,		NULL,			NULL,			NULL,	NULL);

INSERT INTO pothole (latitude, longitude, whoReported, whoInspected, picture, reportDate, inspectDate, repairStartDate, repairEndDate, severity, comment)
VALUES				(41.51077, -81.67545,	2,			NULL,			NULL, '2017-03-31', NULL,		NULL,			NULL,			NULL,	NULL);

INSERT INTO pothole (latitude, longitude, whoReported, whoInspected, picture, reportDate, inspectDate, repairStartDate, repairEndDate, severity, comment)
VALUES				(41.5030861, -81.68936,	2,			NULL,			NULL, '2017-04-07', NULL,		NULL,			NULL,			NULL,	NULL);


--Inspected only
INSERT INTO pothole (latitude, longitude, whoReported, whoInspected, picture, reportDate, inspectDate, repairStartDate, repairEndDate, severity, comment)
VALUES				(41.5052719, -81.67717,		2,			5,			NULL, '2017-01-07', '2017-02-07',		NULL,			NULL,			3,	NULL);

INSERT INTO pothole (latitude, longitude, whoReported, whoInspected, picture, reportDate, inspectDate, repairStartDate, repairEndDate, severity, comment)
VALUES				(41.5102234, -81.65936,		1,			5,			NULL, '2017-02-08', '2017-03-07',		NULL,			NULL,			2,	NULL);

INSERT INTO pothole (latitude, longitude, whoReported, whoInspected, picture, reportDate, inspectDate, repairStartDate, repairEndDate, severity, comment)
VALUES				(41.5068169, -81.6585846,	2,			5,			NULL, '2017-02-01', '2017-02-07',		NULL,			NULL,			1,	NULL);

INSERT INTO pothole (latitude, longitude, whoReported, whoInspected, picture, reportDate, inspectDate, repairStartDate, repairEndDate, severity, comment)
VALUES				(41.50524, -81.6679459,		3,			5,			NULL, '2017-01-18', '2017-03-07',		NULL,			NULL,			5,	NULL);


--Inspected and started to repair
INSERT INTO pothole (latitude, longitude, whoReported, whoInspected, picture, reportDate, inspectDate, repairStartDate, repairEndDate, severity, comment)
VALUES				(41.5045, -81.65811,		4,			5,			NULL, '2017-01-09', '2017-02-07',	'2017-03-07',		NULL,			5,	NULL);

INSERT INTO pothole (latitude, longitude, whoReported, whoInspected, picture, reportDate, inspectDate, repairStartDate, repairEndDate, severity, comment)
VALUES				(41.5019951, -81.6576,		4,			5,			NULL, '2017-01-10', '2017-02-18',	'2017-03-03',		NULL,			4,	NULL);

INSERT INTO pothole (latitude, longitude, whoReported, whoInspected, picture, reportDate, inspectDate, repairStartDate, repairEndDate, severity, comment)
VALUES				(41.5009651, -81.65618,		2,			5,			NULL, '2017-02-07', '2017-03-07',	'2017-03-18',		NULL,			3,	NULL);

INSERT INTO pothole (latitude, longitude, whoReported, whoInspected, picture, reportDate, inspectDate, repairStartDate, repairEndDate, severity, comment)
VALUES				(41.5014153, -81.6516342,	5,			5,			NULL, '2017-02-18', '2017-03-18',	'2017-03-29',		NULL,			5,	NULL);

INSERT INTO pothole (latitude, longitude, whoReported, whoInspected, picture, reportDate, inspectDate, repairStartDate, repairEndDate, severity, comment)
VALUES				(41.5079, -81.68211,		4,			5,			NULL, '2017-01-10', '2017-02-18',	'2017-03-03',		NULL,			4,	NULL);

INSERT INTO pothole (latitude, longitude, whoReported, whoInspected, picture, reportDate, inspectDate, repairStartDate, repairEndDate, severity, comment)
VALUES				(41.49736, -81.6841,		2,			5,			NULL, '2017-02-07', '2017-03-07',	'2017-03-18',		NULL,			3,	NULL);

--Repaired
INSERT INTO pothole (latitude, longitude, whoReported, whoInspected, picture, reportDate, inspectDate, repairStartDate, repairEndDate, severity, comment)
VALUES				(41.49968, -81.6668243,		2,			5,			NULL,	'2017-01-25', '2017-02-07',	'2017-03-17',	'2017-03-25',		4,	NULL);

INSERT INTO pothole (latitude, longitude, whoReported, whoInspected, picture, reportDate, inspectDate, repairStartDate, repairEndDate, severity, comment)
VALUES				(41.50026, -81.65726,		3,			5,			NULL,	'2017-02-05', '2017-03-07',	'2017-03-31',	'2017-04-05',		3,	NULL);

INSERT INTO pothole (latitude, longitude, whoReported, whoInspected, picture, reportDate, inspectDate, repairStartDate, repairEndDate, severity, comment)
VALUES				(41.48902, -81.65108,		1,			5,			NULL,	'2017-01-29', '2017-02-09',	'2017-03-01',	'2017-03-31',		5,	NULL);

INSERT INTO pothole (latitude, longitude, whoReported, whoInspected, picture, reportDate, inspectDate, repairStartDate, repairEndDate, severity, comment)
VALUES				(41.50041, -81.69691,		4,			5,			NULL,	'2017-02-27', '2017-03-07',	'2017-03-22',	'2017-04-08',		2,	NULL);

COMMIT TRANSACTION;

select * from pothole