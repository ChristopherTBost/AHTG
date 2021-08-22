import React, { Component } from 'react';

export class HospitalData extends Component {
    static displayName = HospitalData.name;

  constructor(props) {
    super(props);
    this.state = { hospitals: [], loading: true };
  }

  componentDidMount() {
    this.populateHospitals();
  }

  static renderHospitalsTable(hospitals) {
    return (
      <table className='table table-striped' aria-labelledby="tabelLabel">
        <thead>
          <tr>
            <th>Hospital</th>
            <th>Edit</th>
            <th>Delete</th>
          </tr>
        </thead>
        <tbody>
          {hospitals.map(hospital =>
              <tr key={hospital.Name}>
              <td>{hospital.Name}</td>
              <td><a href='./'>Edit</a></td>
              <td><a href='./'>Delete</a></td>
            </tr>
          )}
        </tbody>
      </table>
    );
  }

  render() {
    let contents = this.state.loading
      ? <p><em>Loading...</em></p>
        : HospitalData.renderHospitalsTable(this.state.hospitals);

    return (
      <div>
        <h1 id="tabelLabel" >Hospitals</h1>
            <p>Please choose the Hospital to work on or <a href='./'>click here to create a new one.</a></p>
        {contents}
      </div>
    );
  }

  async populateHospitals() {
    const response = await fetch('hospital');
    const data = await response.json();
    this.setState({ hospitals: data, loading: false });
  }
}
