import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';

const httpOptions = {
    headers: new HttpHeaders({
        "Content-Type": "application/json"
    })
};

@Injectable()
export class RoleService {

    constructor(private http: HttpClient) { }

    getRoles() {
        return this.http.get("/api/roles");
    }

    getRole(id) {
        return this.http.get("/api/roles/" + id);
    }

    update(role) {
        var body = JSON.stringify(body);
        return this.http.put("/api/roles/" + role.id, body, httpOptions);
    }
}