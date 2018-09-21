import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';

const httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable()
export class UserService {

    constructor(private http: HttpClient) { }

    create(user) {
        var body = JSON.stringify(user);
        return this.http.post("/api/users", body, httpOptions);
    }

    update(user) {
        var body = JSON.stringify(user);
        return this.http.put("/api/users/" + user.id, body, httpOptions);
    }

    delete(id) {
        return this.http.delete("/api/users" + id);
    }

    getUsers() {
        return this.http.get("/api/users");
    }

    getUser(id) {
        return this.http.get("/api/users/" + id);
    }
}