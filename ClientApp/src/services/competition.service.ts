import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';

const httpOptions = {
    headers: new HttpHeaders({'Content-Type': 'application/json'})
};

@Injectable()
export class CompetitionService {

    constructor(private http: HttpClient) { }

    getCompetitions() {
        return this.http.get("/api/competitions");
    }

    getCompetition(id) {
        return this.http.get("/api/competitions/" + id);
    }

    create(competition) {
        var body = JSON.stringify(competition);
        return this.http.post("/api/competitions", body, httpOptions);
    }

    update(competition) {
        var body = JSON.stringify(competition);
        return this.http.put("/api/competitions/" + competition.id, body, httpOptions);
    }

    remove(id) {
        return this.http.delete("/api/competitions/" + id);
    }
}