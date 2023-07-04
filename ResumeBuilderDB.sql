CREATE TABLE Users (
  user_id INT AUTO_INCREMENT PRIMARY KEY,
  username VARCHAR(50) NOT NULL,
  email VARCHAR(100) NOT NULL,
  password VARCHAR(255) NOT NULL
);

CREATE TABLE Resumes (
  resume_id INT AUTO_INCREMENT PRIMARY KEY,
  user_id INT NOT NULL,
  title VARCHAR(100),
  creation_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
  last_modified_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  FOREIGN KEY (user_id) REFERENCES Users(user_id)
);

CREATE TABLE PersonalInfo (
  personal_info_id INT AUTO_INCREMENT PRIMARY KEY,
  resume_id INT NOT NULL,
  full_name VARCHAR(100),
  address VARCHAR(200),
  phone_number VARCHAR(20),
  email VARCHAR(100),
  FOREIGN KEY (resume_id) REFERENCES Resumes(resume_id)
);

CREATE TABLE Education (
  education_id INT AUTO_INCREMENT PRIMARY KEY,
  resume_id INT NOT NULL,
  institute_name VARCHAR(100),
  degree VARCHAR(100),
  field_of_study VARCHAR(100),
  start_date DATE,
  end_date DATE,
  FOREIGN KEY (resume_id) REFERENCES Resumes(resume_id)
);

CREATE TABLE WorkExperience (
  experience_id INT AUTO_INCREMENT PRIMARY KEY,
  resume_id INT NOT NULL,
  company_name VARCHAR(100),
  position VARCHAR(100),
  start_date DATE,
  end_date DATE,
  description TEXT,
  FOREIGN KEY (resume_id) REFERENCES Resumes(resume_id)
);

CREATE TABLE Skills (
  skill_id INT AUTO_INCREMENT PRIMARY KEY,
  skill_name VARCHAR(100)
);

CREATE TABLE ResumeSkills (
  resume_id INT NOT NULL,
  skill_id INT NOT NULL,
  PRIMARY KEY (resume_id, skill_id),
  FOREIGN KEY (resume_id) REFERENCES Resumes(resume_id),
  FOREIGN KEY (skill_id) REFERENCES Skills(skill_id)
);

CREATE TABLE Certificates (
  certificate_id INT AUTO_INCREMENT PRIMARY KEY,
  resume_id INT NOT NULL,
  certificate_name VARCHAR(100),
  issuing_organization VARCHAR(100),
  issue_date DATE,
  FOREIGN KEY (resume_id) REFERENCES Resumes(resume_id)
);

CREATE TABLE Languages (
  language_id INT AUTO_INCREMENT PRIMARY KEY,
  language_name VARCHAR(100),
  proficiency_level VARCHAR(50)
);

CREATE TABLE ResumeLanguages (
  resume_id INT NOT NULL,
  language_id INT NOT NULL,
  PRIMARY KEY (resume_id, language_id),
  FOREIGN KEY (resume_id) REFERENCES Resumes(resume_id),
  FOREIGN KEY (language_id) REFERENCES Languages(language_id)
);

CREATE TABLE Locations (
  location_id INT AUTO_INCREMENT PRIMARY KEY,
  city VARCHAR(100),
  state VARCHAR(100),
  country VARCHAR(100)
);

CREATE TABLE ResumeLocations (
  resume_id INT NOT NULL,
  location_id INT NOT NULL,
  PRIMARY KEY (resume_id, location_id),
  FOREIGN KEY (resume_id) REFERENCES Resumes(resume_id),
  FOREIGN KEY (location_id) REFERENCES Locations(location_id)
);

CREATE TABLE Templates (
  template_id INT AUTO_INCREMENT PRIMARY KEY,
  template_name VARCHAR(100),
  template_file_path VARCHAR(255)
);

CREATE TABLE ResumeTemplates (
  resume_id INT NOT NULL,
  template_id INT NOT NULL,
  PRIMARY KEY (resume_id, template_id),
  FOREIGN KEY (resume_id) REFERENCES Resumes(resume_id),
  FOREIGN KEY (template_id) REFERENCES Templates(template_id)
);
