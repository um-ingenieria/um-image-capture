-- DROP TABLE IAPS_ALL_SUBJECTS;

CREATE TABLE IAPS_ALL_SUBJECTS (
  description VARCHAR(255) NOT NULL,
  id_iaps FLOAT NOT NULL,
  valence_mean  FLOAT NOT NULL,
  valence_standard_deviation  FLOAT NOT NULL,
  arousal_mean  FLOAT NOT NULL,
  arousal_standard_deviation  FLOAT NOT NULL,
  dominance_1_mean  FLOAT,
  dominance_1_standard_deviation  FLOAT ,
  dominance_2_mean  FLOAT,
  dominance_2_standard_deviation  FLOAT,
  set_id INT NOT NULL,
  duration INT not null default 2000,
);

CREATE TABLE IAPS_MALE_SUBJECTS (
  description VARCHAR(255) NOT NULL,
  id_iaps FLOAT NOT NULL,
  valence_mean  FLOAT NOT NULL,
  valence_standard_deviation  FLOAT NOT NULL,
  arousal_mean  FLOAT NOT NULL,
  arousal_standard_deviation  FLOAT NOT NULL,
  dominance_1_mean  FLOAT,
  dominance_1_standard_deviation  FLOAT ,
  dominance_2_mean  FLOAT,
  dominance_2_standard_deviation  FLOAT,
  set_id INT NOT NULL,
  duration INT not null default 2000,
);

CREATE TABLE IAPS_FEMALE_SUBJECTS (
  description VARCHAR(255) NOT NULL,
  id_iaps FLOAT NOT NULL,
  valence_mean  FLOAT NOT NULL,
  valence_standard_deviation  FLOAT NOT NULL,
  arousal_mean  FLOAT NOT NULL,
  arousal_standard_deviation  FLOAT NOT NULL,
  dominance_1_mean  FLOAT,
  dominance_1_standard_deviation  FLOAT ,
  dominance_2_mean  FLOAT,
  dominance_2_standard_deviation  FLOAT,
  set_id INT NOT NULL,
  duration INT not null default 2000,
);