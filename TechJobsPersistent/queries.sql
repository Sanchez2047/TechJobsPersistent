--Part 1


--jobs
	--	Id INT PRIMARY KEY AUTO_INCREMENT
	--	Name LONGTEXT
	--	EmployerId INT
	--	FOREIGN KEY (EmployerId) REFERENCES employers(Id)

--employers
	--	Id INT PRIMARY KEY AUTO_INCREMENT
	--	Name LONGTEXT
	--	Location LONGTEXT

--skills
	--	Id INT PRIMARY KEY AUTO_INCREMENT
	--	Name LONGTEXT
	--	Description LONGTEXT

--jobSkills
	--	JobId INT 
	--	SkillId INT
	--	FOREIGN KEY (JobId) REFERENCES jobs(Id)
	--	FOREIGN KEY (SkillId) REFERENCES skills(Id)

--Part 2


--SELECT Name 
--FROM employers
--WHERE Location = "St. Louis City";


--Part 3



