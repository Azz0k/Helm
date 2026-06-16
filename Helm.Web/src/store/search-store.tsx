import {makeAutoObservable} from "mobx";
import React from "react";

export default class SearchStore {
  constructor() {
    makeAutoObservable(this);
  }
  disabled = true;
  searchValue: string = "";

  disableSearch = () =>{
    this.disabled = true;
  }
  enableSearch = () =>{
    this.disabled = false;
  }
  handleChangeSearchValue = (e: React.ChangeEvent<HTMLInputElement>)=> {
    this.searchValue = e.target.value;
  }
}