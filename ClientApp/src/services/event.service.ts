import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';

const httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable()
export class EventService {

    constructor(private http: HttpClient) { }

    getEvents(filter) {
        return this.http.get("/api/events?" + this.toQueryString(filter));
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

    updateEventCompetition(e) {
        var body = JSON.stringify(e);
        return this.http.put("/api/events/" + e.eventId + "/competitions/" + e.competitionId, body, httpOptions);
    }

    getEventCompetitionParticipants(eventId, competitionId, filter) {
        return this.http.get("/api/events/" + eventId + "/competitions/" + competitionId + "/participants?" + this.toQueryString(filter));
    }

    
    private toQueryString(obj) {
        var parts = [];
        for (var prop in obj) {
            var value = obj[prop];
            if (value != null && value != undefined)
                parts.push(encodeURIComponent(prop) + '=' + encodeURIComponent(value));
        }
        console.log(parts.join('&'));
        return parts.join('&');
    }
}