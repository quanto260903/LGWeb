'use client';

import React, { useState } from 'react';
import Image from 'next/image';
import Link from 'next/link';
import '../css/pages/_contact.scss';
import bannercontact from './../../app/images/banner-contact.png';
import ContactInformation from './../../components/ContactInformation';
import '../css/abstracts/_variables.scss';

import { PostContact } from '@/api/contact'; // Đảm bảo import PostContact đúng
import { ToastContainer, toast } from 'react-toastify'; // Import ToastContainer và toast
import 'react-toastify/dist/ReactToastify.css'; // Import css của react-toastify

export default function MethodResources() {
  const [formData, setFormData] = useState({
    name: '',
    email: '',
    phone: '',
    message: '',
  });
  const [formMessage, setFormMessage] = useState<string | null>(null); // To store form success or error message

  // Handle form input changes
  const handleInputChange = (e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
    const { name, value } = e.target;
    setFormData({
      ...formData,
      [name]: value,
    });
  };

  // Handle form submission
  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault(); // Prevent page reload on form submit

    // Basic form validation (simple check for required fields)
    if (!formData.name || !formData.email || !formData.message) {
      setFormMessage("Please fill in all required fields.");
      return;
    }

    try {
      // Gửi yêu cầu POST đến API của backend
      const result = await PostContact(formData); // Gọi API PostContact để gửi formData

      // Kiểm tra phản hồi từ server
      setFormMessage(result.message || "Thank you for reaching out! We will get back to you soon.");
      setFormData({ name: '', email: '', phone: '', message: '' }); // Reset form data

      // Hiển thị thông báo thành công bằng toast
      toast.success("Your message has been sent successfully!"); // Đây là thông báo thành công
    } catch (error) {
      // Xử lý lỗi khi không thể kết nối với API
      setFormMessage("Error submitting form. Please try again later.");
      console.error(error);

      // Hiển thị thông báo lỗi bằng toast
      toast.error("Error submitting form. Please try again later."); // Đây là thông báo lỗi
    }
  };

  return (
    <div className="contactPage">
      {/* Banner Section */}
      <div className="about-container-img">
        <div className="banner">
          <Link href={'/'}>
            <Image src={bannercontact} alt="Contact Banner" className="background-image" />
          </Link>
        </div>
        <div className="overlay-text">Contacts</div>
      </div>

      {/* Contact Section */}
      <div className="contactSection wrp">
        <div className="breadcrumb">
          <span className='tl'>\ Contacts \</span>
        </div>

        <h2 className='tl2'>Get in touch with us</h2>

        <div className="contactContent">
          {/* Form Section */}
          <div className="contactForm">
            <form onSubmit={handleSubmit}>
              <input type="text" name="name" value={formData.name} placeholder="Name" onChange={handleInputChange} />
              <input type="email" name="email" value={formData.email} placeholder="Email" onChange={handleInputChange} />
              <input type="tel" name="phone" value={formData.phone} placeholder="Phone" onChange={handleInputChange} />
              <textarea
                name="message" value={formData.message} placeholder="Your Message" onChange={handleInputChange}
              ></textarea>
              <button type="submit" className='read-more-btn tl4'>Send Now</button>
            </form>
          </div>

          {/* Contact Information Section */}
          <ContactInformation />
        </div>
      </div>
      {/* Container for Toast Notifications */}
      <ToastContainer position="top-right" autoClose={5000} hideProgressBar={false} closeOnClick pauseOnHover />
    </div>
  );
}
