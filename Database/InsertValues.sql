/* rtID,rtType */
INSERT INTO RelationshipType VALUES (1,'Spouse'),
(2,'Parent');

/* rID,rName,rMBHeight,rFBHeight,hcID,ecID,scID,rStr,rInt,rDex,rCon,rWis,rLuk,rWis */
INSERT INTO Race VALUES(1,'Human',50,47,1,1,1,0,0,0,0,0,0,0),
(2,'Orc',60,60,1,1,1,1,0,0,0,0,0,0),
(3,'Elf',47,51,1,1,1,0,1,0,0,0,0,0),
(4,'Gnome',30,28,1,1,1,0,0,1,0,0,0,0),
(5,'Dwarf',38,34,1,1,1,0,0,0,1,0,0,0),
(6,'Dragonkin',72,62,1,1,1,0,0,0,0,1,0,0),
(7,'Teifling',48,50,1,1,1,0,0,0,0,0,1,0),
(8,'Halfling',35,36,1,1,1,0,0,0,0,0,0,1),
(9,'Dead',0,0,1,1,1,-100,-100,-100,-100,-100,-100,-100);

/* hcID,hcColor1,hcColor2,hcColor3,hcColor4,hcColor5,hcColor6 */
INSERT INTO HairColor VALUES (0,'Red','Grey','Blonde','Auburn','Brown','Black'),
(1,'Green','Auburn','Brown','Blonde','Black','Grey'),
(2,'None','None','None','None','None','Mone'),
(3,'Green','Blue','Pink','Purple','Black','Brown');

/* ecID,ecColor1,ecColor2,ecColor3,ecColor4,ecColor5,ecColor6 */
INSERT INTO EyeColor VALUES (0,'Grey','Amber','Blue','Green','Hazel','Brown'),
(1,'Red','Blue','Gold','Grey','Green','Hazel'),
(2,'Red','Black','Gold','White','Silver','None'),
(3,'None','None','None','None','None','Mone');

/* scID,scColor1,scColor2,scColor3,scColor4,scColor5,scColor6 */
INSERT INTO SkinColor VALUES (0,'Albino','Pale','White','Tan','Brown','Black'),
(1,'Grey','Ashen','White','Green','Red','Brown'),
(2,'Black','Silver','Blue','Green','Gold','Bronze'),
(3,'Purple','Red','Brick','Tan','Brown','Black'),
(4,'None','None','None','None','None','Mone');