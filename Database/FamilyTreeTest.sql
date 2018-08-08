CREATE TABLE Cactor(
    c_ID int PRIMARY KEY NOT NULL,
    c_Name text NOT NULL
);

CREATE TABLE RelationshipType(
    rt_ID int Primary Key NOT NULL,
    rt_type text
);

CREATE TABLE FamilyTree(
    Person_ID int NOT NULL,
    rt_ID int NOT NULL,
    Relation_ID int NOT NULL    
);

INSERT INTO Cactor VALUES (1,'John');
INSERT INTO Cactor VALUES (2,'Jane');
INSERT INTO Cactor VALUES (3,'Eric');
INSERT INTO Cactor VALUES (4,'Erin');
INSERT INTO Cactor VALUES (5,'Adam');
INSERT INTO Cactor VALUES (6,'Abby');
INSERT INTO Cactor VALUES (7,'Bobby');
INSERT INTO Cactor VALUES (8,'Brooke');
INSERT INTO Cactor VALUES (9,'Jim');
INSERT INTO Cactor VALUES (10,'Erica');
INSERT INTO Cactor VALUES (11,'Aaron');
INSERT INTO Cactor VALUES (12,'Bridgette');
INSERT INTO Cactor VALUES (13,'Kate');
INSERT INTO Cactor VALUES (14,'Chris');
INSERT INTO Cactor VALUES (15,'Kim');

INSERT INTO RelationshipType VALUES (1,'Spouse');
INSERT INTO RelationshipType VALUES (2,'Parent');
INSERT INTO RelationshipType VALUES (3,'Child');

INSERT INTO FamilyTree VALUES (1,1,2);
INSERT INTO FamilyTree VALUES (2,1,1);
INSERT INTO FamilyTree VALUES (1,2,9);
INSERT INTO FamilyTree VALUES (2,2,9);
INSERT INTO FamilyTree VALUES (3,1,4);
INSERT INTO FamilyTree VALUES (4,1,3);
INSERT INTO FamilyTree VALUES (3,2,10);
INSERT INTO FamilyTree VALUES (4,2,10);
INSERT INTO FamilyTree VALUES (5,1,6);
INSERT INTO FamilyTree VALUES (6,1,5);
INSERT INTO FamilyTree VALUES (5,2,11);
INSERT INTO FamilyTree VALUES (6,2,11);
INSERT INTO FamilyTree VALUES (7,1,8);
INSERT INTO FamilyTree VALUES (8,1,7);
INSERT INTO FamilyTree VALUES (7,2,12);
INSERT INTO FamilyTree VALUES (8,2,12);
INSERT INTO FamilyTree VALUES (9,1,10);
INSERT INTO FamilyTree VALUES (10,1,9);
INSERT INTO FamilyTree VALUES (9,2,13);
INSERT INTO FamilyTree VALUES (10,2,13);
INSERT INTO FamilyTree VALUES (11,1,12);
INSERT INTO FamilyTree VALUES (12,1,11);
INSERT INTO FamilyTree VALUES (11,2,14);
INSERT INTO FamilyTree VALUES (12,2,14);
INSERT INTO FamilyTree VALUES (13,1,14);
INSERT INTO FamilyTree VALUES (14,1,13);
INSERT INTO FamilyTree VALUES (13,2,15);
INSERT INTO FamilyTree VALUES (14,2,15);