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

    getTeams() {
        return this.http.get("/api/teams");
    }

    getTeam(id) {
        return this.http.get("/api/teams/" + id);
    }

    create(team) {
        var body = JSON.stringify(team);
        return this.http.post("/api/teams", body, httpOptions);
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
}