from flask import Flask, request, jsonify
from flask_cors import CORS
import sqlite3
import pandas as pd
from sklearn.metrics.pairwise import euclidean_distances
from sklearn.preprocessing import MinMaxScaler

app = Flask(__name__)
CORS(app)

def get_db_recommendations(song_title):
    try:
        conn = sqlite3.connect('music.db')
        df = pd.read_sql_query("SELECT * FROM Songs", conn)
        conn.close()

        if df.empty:
            print("Baza de date este goala!")
            return []
        clean_query = song_title.split('(')[0].split('ft.')[0].split('feat.')[0].strip().lower()

        source_df = df[df.apply(lambda row: str(row['Title']).lower() in clean_query or 
                                           clean_query in str(row['Title']).lower(), axis=1)]

        if source_df.empty:
            print(f"Nicio potrivire în DB pentru: {clean_query}")
            return []

        source_song_row = source_df.iloc[0]
        selected_artist = str(source_song_row['Artist']).lower().strip()
        
        scaler = MinMaxScaler()
        features_data = df[['Tempo', 'Energy']]
        features_scaled = scaler.fit_transform(features_data)
        idx = source_song_row.name 
        
        source_features = features_scaled[idx].reshape(1, -1)
        distances = euclidean_distances(source_features, features_scaled).flatten()
        
        sim_indices = distances.argsort()
        
        results = []
        seen_artists = {selected_artist}
        
        for i in sim_indices[1:]:
            candidate = df.iloc[i]
            cand_title = str(candidate['Title']).lower()
            cand_artist = str(candidate['Artist']).lower().strip()

            if cand_artist not in seen_artists and clean_query not in cand_title:
                results.append({
                    "title": candidate['Title'], 
                    "artist": candidate['Artist']
                })
                seen_artists.add(cand_artist)

            if len(results) >= 5: 
                break
        
        return results

    except Exception as e:
        print(f"Error in get_db_recommendations: {e}")
        import traceback
        traceback.print_exc() 
        return []

@app.route('/recommend', methods=['GET'])
def recommend():
    title = request.args.get('title')
    if not title: 
        return jsonify([])
    return jsonify(get_db_recommendations(title))

if __name__ == "__main__":
    app.run(port=5000, debug=True)