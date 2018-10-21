import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';

const httpOptions = {
    headers: new HttpHeaders({ "Content-Type": "application/json" })
}

@Injectable()
export class TeamService {

    constructor(
        private http: HttpClient,
    ) { }

    getTeams(filter) {
        return this.http.get("/api/teams?" + this.toQueryString(filter));
    }

    getTeam(id) {
        return this.http.get("/api/teams/" + id);
    }

    create(team) {
        var body = JSON.stringify(team);
        return this.http.post("/api/teams", body, httpOptions);
    }
    update(team) {
        var body = JSON.stringify(team);
        return this.http.put("/api/teams/" + team.id, body, httpOptions);
    }

    remove(id) {
        return this.http.delete("/api/teams/" + id);
    }

    addUser(email, teamId) {
        var body = JSON.stringify(email);
        return this.http.put("/api/add/" + teamId, body, httpOptions);
    }

    removeUser(userId, teamId) {
        //var body = JSON.stringify(email);
        return this.http.put("/api/remove/" + teamId, userId, httpOptions);
    }

    userStatus(userId, teamId) {
        return this.http.put("/api/status/" + teamId, userId, httpOptions);
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