# JobSeekerHelper - Backend for an application designed to simplyfy job searching

## System arcitecture

[Click here](https://viewer.diagrams.net/?tags=%7B%7D&lightbox=1&highlight=0000ff&layers=1&nav=1&title=JobSeekerBotRevised.drawio#Uhttps%3A%2F%2Fdrive.google.com%2Fuc%3Fid%3D1M0e7G1VD0-sdWkK3opNxTavran0wpTkb%26export%3Ddownload)

## User Features

A user should be able to:

- Register
- Log in/out
- Delete their account
- Set preferences for:
  - The geographic area they are searching in
  - The type of job they are looking for
  - How often the application should find jobs
- Upload their CV and other relevant information for job applications

## Application Functionality

The application should:

- Regularly scan search engines for jobs based on the user's preferences
- Read job postings, research the company posting the job, and tailor a personalized cover letter based on the user's CV and profile for that specific position
- Compile all found jobs and generated personalized cover letters into a report and email the user with the results

## Future Functionality

Features that could be implemented in the future:

- **CV Generation:** Based on the user's uploaded CV, the application selects the most relevant experiences and generates a PDF CV tailored to the specific job.
- **CV Scanning:** Instead of manually entering their CV into the application, users can upload an image/PDF/text of their current CV. The application reads and stores it in the database.
- **Application Review:**
  - A feature that reads the user's personalized cover letter, CV, and the job posting to determine if the user is likely to proceed in the recruitment process.
  - If the application determines the user is not likely to proceed, it sends feedback to the generation service to revise the cover letter and CV, which are then returned to the "reviewer."
  - This process continues until an approval is given.
