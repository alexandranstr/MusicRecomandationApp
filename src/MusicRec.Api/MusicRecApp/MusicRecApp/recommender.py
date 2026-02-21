import sqlite3
import pandas as pd
import sys
from sklearn.metrics.pairwise import euclidean_distances

try:
    
    conn = sqlite3.connect('music.db')
    
    df = pd.read_sql_query("SELECT * FROM Songs", conn)
    conn.close()
except Exception as e:
    print(f"Database error: {e}")
    exit()

if df.empty:
    print("Database is empty! Please add some songs first.")
    exit()

def recommend(song_title):
   
    if song_title not in df['Title'].values:
        print(f"Song '{song_title}' not found in the database!")
        return

    features = df[['Tempo', 'Energy']]
    
    dist_matrix = euclidean_distances(features)
   
    idx = df[df['Title'] == song_title].index[0]
    
    sim_scores = list(enumerate(dist_matrix[idx]))
    sim_scores = sorted(sim_scores, key=lambda x: x[1])
    
    print(f"\n--- Recommendations for '{song_title}' ---")
   
    for i in range(1, 3):
        m_idx = sim_scores[i][0]
        print(f"Recommendation {i}: {df.iloc[m_idx]['Title']} by {df.iloc[m_idx]['Artist']} (Distance: {sim_scores[i][1]:.2f})")

if __name__ == "__main__":
    
    if len(sys.argv) > 1:
        song_name = " ".join(sys.argv[1:])
        recommend(song_name)
    else:
        print("Please provide a song title as an argument.")