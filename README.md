# Music Discovery App (Hybrid C# & Python)

### About the Project
This project is a Music Discovery System designed to provide personalized song recommendations based on acoustic features. Unlike traditional search engines, this app uses a hybrid architecture and Machine Learning to find tracks that "feel" similar to the one the user is currently listening to, focusing on technical attributes like Tempo and Energy.

### Tech Stack & Tools
* **Backend Core:** .NET Core API (C#) - Manages the business logic, user requests, and orchestrates communication between services.
* **AI/Recommendation Engine:** Python (Flask) - A specialized microservice that handles data processing and mathematical computations.
* **Data Science Libraries:** * `Pandas`: For data manipulation and database querying.
    * `Scikit-learn`: Used for `MinMaxScaler` (normalizing audio features) and `Euclidean Distances` (calculating song similarity).
* **Integration:** `YoutubeExplode` - A powerful library used in C# to fetch high-quality metadata and stream links directly from YouTube.
* **Database:** `SQLite` - A lightweight, efficient relational database storing song metadata.

### How the Recommendation Logic Works
1.  **Acoustic Analysis:** Each song in the database has numerical values for `Tempo` (BPM) and `Energy` (0.0 to 1.0).
2.  **Vector Space:** When a user selects a song, the Python service maps it into a 2D coordinate system.
3.  **Mathematical Similarity:** The system calculates the **Euclidean Distance** between the selected song and all other songs in the database.
    * *Formula used:* $d(p,q) = \sqrt{(p_1-q_1)^2 + (p_2-q_2)^2}$
4.  **Discovery Filter:** To ensure variety, the algorithm excludes songs from the same artist and prioritizes "Discovery Mode," finding the nearest neighbors in the vector space that belong to different creators.

### Smart YouTube Filtering
To ensure a professional "Music-Only" experience, the system implements a multi-layer filter:
* **Query Refinement:** Automatically appends `-live -reaction -review` to search queries.
* **Metadata Validation:** In C#, results are filtered by duration (excluding videos > 7 mins) and title keywords to prevent vlogs or "reaction" videos from appearing.

---


### Despre Proiect
Acest proiect este un sistem de descoperire muzicală, conceput pentru a oferi recomandări personalizate bazate pe caracteristici acustice. Spre deosebire de motoarele de căutare tradiționale, această aplicație folosește o arhitectură hibridă și Machine Learning pentru a găsi piese care au un "vibe" similar cu cea curentă, concentrându-se pe atribute tehnice precum Tempoul și Energia.

### Tehnologii și Instrumente
* **Backend Core:** .NET Core API (C#) - Gestionează logica de business, cererile utilizatorilor și orchestrează comunicarea între servicii.
* **Motorul de Recomandare AI:** Python (Flask) - Un microserviciu specializat care se ocupă de procesarea datelor și calculele matematice.
* **Biblioteci Data Science:** * `Pandas`: Pentru manipularea datelor și interogarea bazei de date.
    * `Scikit-learn`: Folosit pentru `MinMaxScaler` (normalizarea caracteristicilor audio) și `Euclidean Distances` (calcularea similitudinii).
* **Integrare:** `YoutubeExplode` - O librărie performantă folosită în C# pentru a prelua metadate de înaltă calitate și link-uri direct de pe YouTube.
* **Baza de Date:** `SQLite` - O bază de date relațională ușoară care stochează metadatele pieselor.

### Cum Funcționează Logica de Recomandare
1.  **Analiza Acustică:** Fiecare piesă din baza de date are valori numerice pentru `Tempo` (BPM) și `Energie` (0.0 la 1.0).
2.  **Spațiu Vectorial:** Când un utilizator selectează o piesă, serviciul Python o mapează într-un sistem de coordonate 2D.
3.  **Similitudine Matematică:** Sistemul calculează **Distanța Euclidiană** între piesa selectată și toate celelalte piese din baza de date.
    * *Formula utilizată:* $d(p,q) = \sqrt{(p_1-q_1)^2 + (p_2-q_2)^2}$
4.  **Filtru Discovery:** Pentru a asigura varietatea, algoritmul exclude piesele de la același artist și prioritizează "Modul Discovery", găsind cei mai apropiați vecini din spațiul vectorial care aparțin unor creatori diferiți.

### Filtrare Inteligentă YouTube
Pentru a asigura o experiență profesională de tip "Doar Muzică", sistemul implementează un filtru multi-strat:
* **Rafinarea Interogării:** Adaugă automat `-live -reaction -review` la căutările pe YouTube.
* **Validarea Metadatelor:** În C#, rezultatele sunt filtrate după durată (excluzând clipurile > 7 min) și cuvinte cheie din titlu pentru a preveni apariția vlogurilor sau a clipurilor de tip "reacție".

---
### Screenshots
<img width="1039" height="456" alt="image" src="https://github.com/user-attachments/assets/6478bcbb-e30e-4825-b1fa-8af798eb5d55" />

<img width="1039" height="448" alt="image" src="https://github.com/user-attachments/assets/7862d6d9-7b78-41d4-bdad-17f887e580b4" />

---
**Developed by Nistor Alexandra Meda**
