# Week 10 - Day 3: Presentation Rehearsal & Live Demo Practice

## Overview

In this day, I prepared for the final project presentation by focusing on presentation rehearsal, live demo planning, technical question preparation, and presentation timing.

Because the team project is still under active development and the final presentation is not yet complete, the final timed rehearsal and complete live demo were documented as preparation plans rather than completed activities.

The preparation also included drafting likely technical questions and answers related to the Trip Planning Platform architecture, backend-to-FastAPI integration, testing approach, scalability considerations, and future improvements.

---

## Presentation Rehearsal Plan

A full timed rehearsal will be performed after the final project presentation is completed.

The rehearsal will include:

- Running the complete presentation from start to finish.
- Explaining each slide out loud instead of reading directly from the screen.
- Measuring the total presentation time.
- Practicing the transition between presentation sections.
- Performing the live demo during the rehearsal.
- Identifying sections that need shorter or clearer explanations.
- Repeating the rehearsal after adjusting the presentation pacing.

This follows the same preparation approach used before previous project discussions where the presentation had to fit within a specific time limit.

---

## Live Demo Practice Plan

The final live demo will be practiced after the team project reaches its completed state.

The demo practice will include:

- Testing the real project flow before the presentation.
- Verifying that the required backend and planning services are available.
- Running the intended demonstration scenario from start to finish.
- Preparing for possible network, hosting, or service delays.
- Keeping a backup demo recording available in case the live demo cannot be completed during the presentation.

The backup recording will act as a fallback plan if a technical issue occurs during the live presentation.

---

## Technical Q&A Preparation

### 1. Why did we use ASP.NET Core and a separate FastAPI planning service?

ASP.NET Core is used for the main backend API and overall system integration, while FastAPI handles the planning-related service.

Separating these responsibilities keeps each service focused on its role and makes the integration easier to test and validate independently.

---

### 2. What happens if FastAPI returns an invalid response?

The backend validates the response received from FastAPI before using it.

If the response structure or planning result is invalid, the backend rejects it instead of allowing incorrect data to continue through the system.

---

### 3. How did we verify the ASP.NET Core and FastAPI integration?

The integration was verified through automated tests and real integration tests between the backend and the FastAPI planning service.

The tests covered both successful planning flows and invalid-response scenarios to make sure the backend handles the integration reliably.

---

### 4. How would the system handle significantly higher load?

The current implementation focuses on correctness and reliable integration.

If the system needs to support significantly higher traffic, the services could be scaled independently, and additional techniques such as caching or asynchronous processing could be introduced based on measured bottlenecks.

Any scaling decision should be based on actual performance measurements rather than assumptions.

---

### 5. What would we improve with more time?

With more development time, the project could be improved through broader automated test coverage, clearer performance measurements, stronger monitoring, and additional scalability work.

Future improvements should be prioritized based on actual project usage and measured system behavior rather than assumptions.

---

## Second Rehearsal & Mock Q&A Plan

A second full rehearsal will be completed after the final project presentation and live demo are ready.

This rehearsal will include:

- Presenting the complete project presentation from start to finish.
- Performing the final live demo.
- Checking the presentation timing again after the first rehearsal adjustments.
- Practicing the prepared technical questions and answers.
- Reviewing unclear explanations or weak transitions.
- Adjusting the presentation based on feedback before the final discussion.

The mock Q&A will focus on the project's architecture, service integration, testing approach, scalability considerations, and possible future improvements.

---

## Presentation Timing Verification Plan

The final presentation timing will be verified after the project, presentation deck, and live demo are fully completed.

The final timing check will include:

- Running the complete presentation from start to finish.
- Including the full live demo in the measured time.
- Recording the total presentation duration.
- Identifying sections that take longer than expected.
- Shortening or simplifying explanations when necessary.
- Repeating the rehearsal until the presentation fits within the allotted discussion time.

The final timing will only be confirmed after the complete project presentation and demo are ready.

---

## Key Takeaways

- A presentation should be rehearsed out loud and timed before the final discussion.
- A live demo should be practiced using the real system.
- A backup demo recording should be available in case of technical issues.
- Technical questions should be prepared in advance with clear and honest answers.
- Architectural decisions should be explained using technical reasoning and actual project evidence.
- Future improvements should be discussed honestly without claiming that unimplemented features already exist.
- Presentation timing should be verified only after the final presentation and demo are complete.

---

## Tools Used

- ASP.NET Core
- FastAPI
- GitHub
- Postman
- PowerPoint or Google Slides