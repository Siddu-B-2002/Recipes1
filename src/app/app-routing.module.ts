import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AddRecipeComponent } from './components/recipes/add-recipe/add-recipe.component';
import { EditRecipeComponent } from './components/recipes/edit-recipe/edit-recipe.component';
import { RecipesListComponent } from './components/recipes/recipes-list/recipes-list.component';

const routes: Routes = [
  {
    path: '',
    component: RecipesListComponent
  },
  {
    path: 'recipes',
    component: RecipesListComponent
  },
  {
    path: 'recipes/add',
    component: AddRecipeComponent
  },
  {
    path: 'recipes/edit/:id',
    component: EditRecipeComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
