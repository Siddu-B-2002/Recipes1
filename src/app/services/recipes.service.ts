import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Recipe } from '../models/recipe.model';

@Injectable({
  providedIn: 'root'
})
export class RecipesService {

  constructor(private http: HttpClient) { }

  getAllRecipes(): Observable<Recipe[]>
  {
    return this.http.get<Recipe[]>('https://localhost:7254' + '/api/Recipes');
  }

  addRecipe(addRecipeRequest: Recipe): Observable<Recipe>
  {
    addRecipeRequest.id='00000000-0000-0000-0000-000000000000';
    return this.http.post<Recipe>('https://localhost:7254/api/Recipes', addRecipeRequest);
  }

  getRecipe(id: string): Observable<Recipe>
  {
    return this.http.get<Recipe>('https://localhost:7254/api/Recipes/' + id);
  }

  updateRecipe(id:string, updateRecipeReq: Recipe):Observable<Recipe>
  {
    return this.http.put<Recipe>('https://localhost:7254/api/Recipes/'+ id, updateRecipeReq);
  }

  deleteRecipe(id: string): Observable<Recipe>
  {
    return this.http.delete<Recipe>('https://localhost:7254/api/Recipes/'+ id);
  }
}
