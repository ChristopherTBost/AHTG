import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { HospitalData } from './components/HospitalData';
import { HospitalEdit } from './components/HospitalEdit';
import { HospitalCreate } from './components/HospitalCreate';
import { HospitalDelete } from './components/HospitalDelete';

import './custom.css'

export default class App extends Component {
  static displayName = App.name;

  render () {
    return (
      <Layout>
        <Route exact path='/' component={HospitalData} />
        <Route path='/hospital-data' component={HospitalData} />
        <Route path='/hospital-edit' component={HospitalEdit} />
        <Route path='/hospital-create' component={HospitalCreate} />
        <Route path='/hospital-delete' component={HospitalDelete} />
      </Layout>
    );
  }
}
