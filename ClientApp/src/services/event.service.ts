import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';

const httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable()
export class EventService {

    constructor(private http: HttpClient) { }

    getEvents() {
        return this.http.get("/api/events");
    }

    getEvent(id) {
        return this.http.get("/api/events/" + id);
    }

    create(e) {
        var body = JSON.stringify(e);
        return this.http.post("/api/events", body, httpOptions);
    }

    update(e) {
        var body = JSON.stringify(e);
        return this.http.put("/api/events/" + e.id, body, httpOptions);
    }

    remove(id) {
        return this.http.delete("/api/events/" + id);
    }

    getEventCompetition(eventId, competitionId) {
        return this.http.get("/api/events/" + eventId + "/competitions/" + competitionId);
    }
}