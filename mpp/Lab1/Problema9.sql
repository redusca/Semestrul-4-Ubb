CREATE TABLE "Arbitru" (
  "id" integer PRIMARY KEY,
  "nume" varchar,
  "username" varchar,
  "parola" varchar,
  "proba_asociata" varchar NOT NULL
);

CREATE TABLE "Participanti" (
  "id" integer PRIMARY KEY,
  "nume" varchar,
  "puncte_acumulate" integer
);

CREATE TABLE "Proba" (
  "id" varchar PRIMARY KEY,
  "arbitrul_probei" integer NOT NULL,
  "total_puncte_proba" integer,
  "numar_participanti" integer
);

CREATE TABLE "RezultatParticipant" (
  "id" integer PRIMARY KEY,
  "id_proba" varchar NOT NULL,
  "id_participant" integer NOT NULL,
  "numar_puncte" integer
);

ALTER TABLE "Proba" ADD CONSTRAINT "ArPr" FOREIGN KEY ("id") REFERENCES "Arbitru" ("proba_asociata");

ALTER TABLE "Participanti" ADD CONSTRAINT "PtLeg" FOREIGN KEY ("id") REFERENCES "RezultatParticipant" ("id_participant");

ALTER TABLE "Proba" ADD CONSTRAINT "PbLeg" FOREIGN KEY ("id") REFERENCES "RezultatParticipant" ("id_proba");
