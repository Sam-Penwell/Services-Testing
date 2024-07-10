# Apps / Services / Etc.

The 12Factor app uses "App" to mean a unit of deployment. "One repo, many deploys"
    - This is the repo your team owns that most importantly exposes a way for the "app" to be used (an HTTP endpoint, a UI, etc.)
    - That app can contain:
        - Many classes 
        - Many class libraries
        - Many other services
        - Packages (libraries) it consumes.
        That is all up to you and your team.

    - A "Service" is an "App" that owns some state or state process within an organization.
        - This is the main focus of the Microservices Course.


- Jeff Acknowledging His Sins, Part 1:
    - Early on I was too much on the "Microservices" thing that I pushed it without enough context.


- Why Multiple Services in an App:
    - Instead of sharing binaries (libraries) you can use services to do this.
    - The benefit is that binaries are always early bound.
    - Also they are for a particular programming language/version/etc. 
    - They can be scaled independently from the rest of the app (scale out using the process model).
    - Some parts of your "app" have a different delivery schedule than the rest.
        - for example, new stuff in the app often has a lot of "churn" in the beginning
            - we are figuring it out, screwing up, getting feedback from users/testers, etc.
            - we can isolate that in a service so only that part has to be deployed
                - the pipeline doesn't have to run EVERYTHING, just that part.
                