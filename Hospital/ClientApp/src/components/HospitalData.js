import React, { Component } from 'react';
import { Link } from 'react-router-dom';

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
            <th></th>
            <th></th>
          </tr>
        </thead>
        <tbody>
          {hospitals.map(hospital =>
              <tr key={hospital.title}>
              <td>{hospital.title}</td>
                  <td>
                      <Link tag={Link} className="text-dark" to={"/hospital-edit/" + hospital.id}>Edit</Link>
                  </td>
                  <td>
                      <Link tag={Link} className="text-dark" to={"/hospital-delete/" + hospital.id}>Delete</Link>
                  </td>
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
            <p>Please choose the Hospital to work on or <Link className="text-dark" to={"/hospital-create/"}>click here to create a new one.</Link></p>
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
