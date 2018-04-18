CREATE TABLE Race(
    rID int primary key not null,
    rName text not null,
    rMBHeight int not null,
    rFBHeight int not null,
    hcID int not null,
    ecID int not null,
    scID int not null,
    rStr int not null,
    rInt int not null,
    rDex int not null,
    rCon int not null,
    rWis int not null,
    rLuk int not null,
    rCha int not null
);

CREATE TABLE HairColor(
    hcID int primary key not null,
    hcColor1 text not null,
    hcColor2 text not null,
    hcColor3 text not null,
    hcColor4 text not null,
    hcColor5 text not null,
    hcColor6 text not null
);

CREATE TABLE EyeColor(
    ecID int primary key not null,
    ecColor1 text not null,
    ecColor2 text not null,
    ecColor3 text not null,
    ecColor4 text not null,
    ecColor5 text not null,
    ecColor6 text not null
);

CREATE TABLE SkinColor(
    scID int primary key not null,
    scColor1 text not null,
    scColor2 text not null,
    scColor3 text not null,
    scColor4 text not null,
    scColor5 text not null,
    scColor6 text not null
);