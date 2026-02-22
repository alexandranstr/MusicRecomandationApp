import sqlite3
import pandas as pd
import sys
from sklearn.metrics.pairwise import euclidean_distances
from sklearn.preprocessing import MinMaxScaler

try:
    conn = sqlite3.connect('music.db')
    df = pd.read_sql_query("SELECT * FROM Songs", conn)
    conn.close()
except Exception as e:
    print(f"Database error: {e}")
    exit()

if df.empty:
    print("Database is empty!")
    exit()

def recommend(song_title):
    if song_title not in df['Title'].values:
        print(f"Song '{song_title}' not found!")
        return

    selected_song = df[df['Title'] == song_title].iloc[0]
    selected_genre = selected_song['Genre']
    
    genre_df = df[df['Genre'] == selected_genre].copy()
    
    target_df = genre_df if len(genre_df) > 1 else df

    scaler = MinMaxScaler()
    features = target_df[['Tempo', 'Energy']]
    scaled_features = scaler.fit_transform(features)
    
    dist_matrix = euclidean_distances(scaled_features)
    
    idx = target_df[target_df['Title'] == song_title].index[0]
    
    pos = target_df.index.get_loc(idx)
    
    sim_scores = list(enumerate(dist_matrix[pos]))
    sim_scores = sorted(sim_scores, key=lambda x: x[1])
    
    print(f"\n--- Recommendations for '{song_title}' [{selected_genre}] ---")
    
    count = 0
    for i in range(1, len(sim_scores)):
        if count >= 3: break
        
        m_idx = sim_scores[i][0]
        song_data = target_df.iloc[m_idx]
        
        print(f"{count+1}. {song_data['Title']} by {song_data['Artist']} (Distance: {sim_scores[i][1]:.4f})")
        count += 1

if __name__ == "__main__":
    if len(sys.argv) > 1:
        recommend(" ".join(sys.argv[1:]))
    else:
        print("Please provide a song title.")