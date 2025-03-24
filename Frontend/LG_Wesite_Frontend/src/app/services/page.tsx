import Image from "next/image";
import '../css/pages/_services.scss';
import productImage from './../../app/images/products.png'
import outsourceImage from './../../app/images/outsource.png'
import supportImage from './../../app/images/support.png'
import techImage from './../../app/images/backgroundservices.png'
import iconTech from './../../app/images/Tech-Vector.png'
import OurExpertise from "./../../components/Service";
import Technologies  from "./../../components/Technologies";
export default function Services() {
  return (
    <div className="services-page">
      {/* Section Header */}
      <div className="service-header">
      <div className="image-overlay">
        <Image
          src={productImage}
          alt="Software development"
          className="background-image"
        />
        </div>
        <div className="overlay-text">Services</div>
      </div>

      {/* Main Content */}
    <OurExpertise showButton={false} isHomePage={false} />

     <Technologies/>
    </div>
  );
}
